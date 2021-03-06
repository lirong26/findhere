package dao;

import java.util.List;

import model.Content;

public interface ContentDao {
	public Content findOneByID(String contentID);
	public List<Content> getContentsByTargetID(String targetID);
	public String insertOne(Content content);
	public void deleteOne(String contentID);
}
