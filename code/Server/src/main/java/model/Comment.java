package model;

import java.io.Serializable;
import java.util.Date;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

@Document(collection = "comments")
public class Comment implements Serializable {
	private static final long serialVersionUID = 1L;

	@Id
	private ObjectId _id;
	@Field("userID")
	private String userID;
	@Field("type")
	private String type;
	@Field("targetID")
	private String targetID;
	@Field("time")
	private Date time;
	@Field("text")
	private String text;
	@Field("soundID")
	private String soundID;
	@Field("pictureID")
	private String pictureID;
	
	public Comment(){}
	
	/*GET and SET*/
	public ObjectId get_id() {
		return _id;
	}
	
	public void set_id(ObjectId s) {
		this._id = s;
	}
	
	public String getUserID() {
		return userID;
	}
	
	public void setUserID(String s) {
		this.userID = s;
	}
	
	public String getType() {
		return type;
	}
	
	public void setType(String s) {
		this.type = s;
	}
	
	public Date getTime() {
		return time;
	}
	
	public void setTime(Date s) {
		this.time = s;
	}
	
	public String getTargetID() {
		return targetID;
	}
	
	public void setTargetID(String s) {
		this.targetID = s;
	}
	
	public String getText() {
		return text;
	}
	
	public void setText(String s) {
		this.text = s;
	}
	
	public String getSoundID() {
		return soundID;
	}
	
	public void setSoundID(String s) {
		this.soundID = s;
	}
	
	public String getPictureID() {
		return pictureID;
	}
	
	public void setPictureID(String s) {
		this.pictureID = s;
	}
}
