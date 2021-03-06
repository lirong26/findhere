package dao.Impl;

import java.util.List;

import javax.annotation.Resource;

import org.bson.types.ObjectId;
import org.springframework.data.domain.Sort;
import org.springframework.data.domain.Sort.Direction;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;

import dao.ContentDao;
import model.Comment;
import model.Content;

public class ContentDaoImpl implements ContentDao {
	
	@Resource
	private MongoTemplate mongoTemplate;
	
	public Content findOneByID(String contentID) {
		Content content = mongoTemplate.findOne(
				new Query(Criteria.where("_id").is(new ObjectId(contentID))), Content.class);
		if(content == null) content = new Content();
		return content;
	}
	
	public List<Content> getContentsByTargetID(String targetID){
		List<Content> contents = mongoTemplate.find(
				new Query(Criteria.where("targetID").is(targetID)), Content.class);
		return contents;
	}
	
	public String insertOne(Content content) {
		mongoTemplate.insert(content);
		return content.getId().toString();
	}
	
	public void deleteOne(String contentID) {
		mongoTemplate.remove(
				new Query(Criteria.where("_id").is(contentID)), Content.class);
	}
	
	/*GET and SET*/
	public MongoTemplate getMongoTemplate() {
		return mongoTemplate;
	}
	
	public void setMongoTemplate(MongoTemplate mongo) {
		mongoTemplate = mongo;
	}
}
