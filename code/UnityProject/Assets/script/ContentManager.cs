﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ContentManager : MonoBehaviour,ITrackableEventHandler {
    private CloudRecoBehaviour mCloudRecoBehaviour;
    //0:scanning 1:infopoint 2:textfield 3:imagefield
    private int status;
    private bool isTrackable;
    private string keepTargetId;
    private string targetId;
    private AudioSource music;
    private bool isAudioShow;

    public GameObject infoPoint;
    public GameObject TextField;
    public GameObject ImageField;
    public GameObject AudioField;

    public InfoLoader infoLoader;
    
    //public Button CancelButton;
    public AnimationsManager AnimationsManager;

    // Use this for initialization
    void Start () {

        TrackableBehaviour trackableBehaviour = infoPoint.transform.parent.GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
        {
            trackableBehaviour.RegisterTrackableEventHandler(this);
        }
        mCloudRecoBehaviour = FindObjectOfType<CloudRecoBehaviour>();
        music = AudioField.GetComponent<AudioSource>();

        //SetCancelButtonVisible(false);
        status = 0;
        targetId = "";
        isTrackable = false;
        isAudioShow = false;
    }

    void Update()
    {
        //声音停止后喇叭消失
        if (music!=null&&!music.isPlaying&&isAudioShow)
        {
            ShowField(AudioField,false);
        }
        //信息点的移动效果
        /*if (status == 1)
        {
            float v = 0.1f*Time.deltaTime;
            foreach(Transform point in infoPoint.transform)
            {
                //Vector3 pos = point.localPosition;
                float x, y, z;
                x = Random.Range(-0.8f, 0.8f);
                y = Random.Range(0, 0.4f);
                z = Random.Range(-0.8f, 0.8f);
                point.localPosition = Vector3.Lerp(point.localPosition, new Vector3(x,y,z), v);
                //确定移动坐标
                if (pos.x <= -0.8f)
                {
                    x = Random.Range(0, v);
                }
                else if (pos.x >= 0.8f)
                {
                    x = Random.Range(-v, 0);
                }
                else
                {
                    x = Random.Range(-v, v);
                }
                if (pos.y <= 0)
                {
                    y = Random.Range(0, v);
                }
                else if (pos.y >= 0.4f)
                {
                    y = Random.Range(-v, 0);
                }
                else
                {
                    y = Random.Range(-v, v);
                }
                if (pos.z <= -1f)
                {
                    z = Random.Range(0, v);
                }
                else if (pos.z >= 1f)
                {
                    z = Random.Range(-v, 0);
                }
                else
                {
                    z = Random.Range(-v, v);
                }
                point.localPosition = new Vector3(pos.x + x, pos.y + y, pos.z + z);

            }
        }*/
        //点击事件处理
        if (Input.GetMouseButtonUp(0))
        {
            if (status==0)
            {
                return;
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject == null)
                {
                    return;
                }
                if (hitObject.transform.parent.tag == "InfoPoint")
                {
                    Debug.Log("touch info point"+hitObject.name);
                    OnDisplay(hitObject);
                }
                else if (hitObject.name == "TextField")
                {
                    Debug.Log("touch text field");
                    AnimationsManager.PlayAnimationTo2D(TextField);
                    TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
                }
                else if(hitObject.name == "ImageContent")
                {
                    Debug.Log("touch image field");
                    AnimationsManager.PlayAnimationTo2D(ImageField);
                    TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
                }
                else if(hitObject.name == "AudioField")
                {
                    Debug.Log("touch audio field");
                    AudioPlaying(false);
                }
                else
                {
                    Debug.Log(hitObject.name);
                }
            }
            else
            {
                OnCancel();
                TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
            }
        }
    }

    public void GetTargetId(string target)
    {
        target = targetId;
    }

    public void Show(bool tf)
    {
        if (status == 1)
        {
            ShowField(infoPoint,tf);
            //AudioPlaying(false);
        }
        else if (status== 2)
        {
            if (tf)
            {
                AnimationsManager.PlayAnimationTo3D(TextField);
            }
            else
            {
                AnimationsManager.PlayAnimationTo2D(TextField);
            }
        }
        else if (status == 3)
        {
            if (tf)
            {
                AnimationsManager.PlayAnimationTo3D(ImageField);
            }
            else
            {
                AnimationsManager.PlayAnimationTo2D(ImageField);
            }
        }
    }

    public void AudioPlaying(bool tf)
    {
        if (status == 1)
        {
            ShowField(AudioField, tf);
        }
        if (tf)
        {
            music.Play();
        }
        else
        {
            music.Stop();
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            isTrackable = true;
            targetId = keepTargetId;
            Show(true);
            Debug.Log("content manager show: true");
        }
        else
        {
            isTrackable = false;
            targetId = "";
            Show(false);
            Debug.Log("content manager show: false");
        }
        Debug.Log("targetId: " + targetId);
        infoLoader.SetTargetId(targetId);
    }

    public void TargetCreated(string target)
    {
        keepTargetId = target;
        status = 1;
        infoLoader.LoadInfoPoint(target);

        /*for(int i = 0; i < 10; i++)
        {
            GameObject sphere = Instantiate(textInfo);
            sphere.transform.parent = infoPoint.transform;
            sphere.transform.position = new Vector3(
                parentPosition.x + Random.Range(-1f, 1f), 
                parentPosition.y + Random.Range(0, 0.8f), 
                parentPosition.z + Random.Range(-1f, 1f));
            sphere.transform.name = target + i;
        }
        textInfo.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            GameObject sphere = Instantiate(imageInfo);
            sphere.transform.parent = infoPoint.transform;
            sphere.transform.localPosition = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(0, 0.8f),
                Random.Range(-1f, 1f));
            sphere.transform.name = target + i;
        }
        imageInfo.SetActive(false);
        */
    }

    public void OnCancel()
    {
        //SetCancelButtonVisible(false);
        if (infoLoader.isLoading())
        {
            infoLoader.stopLoading();
        }
        if (isTrackable)
        {
            ShowField(infoPoint,true);
        }
        if (status == 2)
        {
            ShowField(TextField, false);
            AnimationsManager.PlayAnimationTo3D(TextField);

        } 
        else if (status == 3)
        {
            ShowField(ImageField,false);
            AnimationsManager.PlayAnimationTo3D(ImageField);
        }
        status = 1;
    }

    public int GetStatus()
    {
        return status;
    }

    public void SetStatus(int newValue)
    {
        status = newValue;
    }

    public void OnDisplay(GameObject obj)
    {
        string tag = obj.tag;
        if (tag == "TextInfo")
        {
            ShowField(infoPoint, false);
            infoLoader.LoadText(obj.name);
            status = 2;
        }
        else if (tag == "ImageInfo")
        {
            ShowField(infoPoint, false);
            infoLoader.LoadImage(obj.name);
            status = 3;
        }
        else if (tag == "AudioInfo")
        {
            if (!music.isPlaying)
            {
                infoLoader.LoadAudio(obj);
            }
            else if (AudioField.transform.parent == obj.transform.parent)
            {
                AudioPlaying(false);
            }
            else
            {
                infoLoader.LoadAudio(obj);
            }
            status = 1;
        }

        //SetCancelButtonVisible(true);
        
        
    }

    public void ShowField(GameObject Field, bool visible)
    {
        if (Field == AudioField)
        {
            isAudioShow = visible;
        }
        Renderer[] rendererComponents = Field.GetComponentsInChildren<Renderer>();
        Collider[] colliderComponents = Field.GetComponentsInChildren<Collider>();

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = visible;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = visible;
        }
    }

    /*
    private void SetCancelButtonVisible(bool visible)
    {
        if (CancelButton == null) return;

        if (CancelButton.enabled != visible)
        {
            CancelButton.enabled = visible;
            CancelButton.image.enabled = visible;
        }
    }
    */

}
