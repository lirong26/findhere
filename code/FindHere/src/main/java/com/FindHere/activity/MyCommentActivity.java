package com.FindHere.activity;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.ImageButton;
import android.widget.ListView;

import com.FindHere.control.Connect;
import com.FindHere.control.myCommentAdapter;
import com.FindHere.model.Comment;
import com.FindHere.view.LeftDeleteView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.LinkedList;
import java.util.List;

public class MyCommentActivity extends Activity {
    private SharedPreferences sp;
    private String userID;
    private ImageButton backBtn;
    private ImageButton delBtn;
    private List<Comment> mData = null;
    private myCommentAdapter mAdapter = null;
    private ListView mycomment_list;
    private boolean isOpen = false;
    private String jsonStr;
    private String ip;
    private String returnStr;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.mycomment_activity);

        sp = getSharedPreferences("userInfo", Context.MODE_ENABLE_WRITE_AHEAD_LOGGING);
        userID = sp.getString("userID","");

        backBtn = (ImageButton)findViewById(R.id.back);
        backBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

        ip = getString(R.string.my_comment_ip);
        mycomment_list= (ListView)findViewById(R.id.list_view);
        mData = new LinkedList<Comment>();
        mAdapter = new myCommentAdapter((LinkedList<Comment>) mData,this);
        mycomment_list.setAdapter(mAdapter);

        delBtn = (ImageButton)findViewById(R.id.delete);
        delBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                LinkedList<LeftDeleteView> mView = mAdapter.getMyView();
                if(!isOpen){
                    for(LeftDeleteView convertView:mView){
                        convertView.leftScroll();
                        isOpen = true;
                    }
                }else{
                    for(LeftDeleteView convertView:mView){
                        convertView.rightScroll();
                        isOpen = false;
                    }
                }
            }
        });

        JSONObject object = new JSONObject();
        try {
            object.put("userID", userID);
            object.put("pageIndex", 0);
            jsonStr = object.toString();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        new Thread(new Runnable(){
            @Override
            public void run() {
                Connect myConnect = new Connect(MyCommentActivity.this);
                returnStr=myConnect.sendHttpPost(ip,jsonStr);
                Message msg = mHandler.obtainMessage();
                msg.what = 1;
                mHandler.sendMessage(msg);
            }}).start();

    }

    public Handler mHandler = new Handler(){
        public void handleMessage(Message msg) {
            if(msg.what==1) {
                JSONArray myJsonArray;
                try
                {
                    Log.d("hhh",returnStr);
                    myJsonArray = new JSONArray(returnStr);

                    for(int i=0 ; i < myJsonArray.length() ;i++)
                    {
                        JSONObject myObject = myJsonArray.getJSONObject(i);

                        String type = myObject.getString("type");
                        String commentID = myObject.getString("commentID");
                        String text = myObject.optString("text");
                        String time = myObject.getString("time");
                        String userID = myObject.getString("userID");
                        String targetID = myObject.getString("targetID");
                        String targetImage = myObject.optString("image");

                        Log.d("haha",text);
                        Comment myComment = new Comment();
                        myComment.setType(type);
                        myComment.setContentId(commentID);
                        myComment.setText(text);
                        myComment.setTime(time);
                        myComment.setUserId(userID);
                        myComment.setTargetId(targetID);
                        myComment.setTargetImage(Base64.decode(targetImage,Base64.DEFAULT));

                        mData.add(myComment);
                    }
                }
                catch (JSONException e)
                {
                }
                mAdapter.notifyDataSetChanged();
            }
        }
    };

    @Override
    protected void onRestart() {
        super.onRestart();
    }

    @Override
    protected void onStart() {
        super.onStart();
    }

    @Override
    protected void onResume() {
        super.onResume();
    }

    @Override
    protected void onPause() {
        super.onPause();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }
}