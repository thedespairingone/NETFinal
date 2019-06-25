using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NETFinalProj.Dao;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using GetJSON;
namespace NETFinalProj.Services
{
    public class Comment
    {

        //根据电影名称获取最新评论
        public static String GetLatestComments(String title)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("film", title);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("time");   
            //var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            var sort = Builders<BsonDocument>.Sort.Descending("time");
            var documents = GetConnectComment.collection.Find(filter).Sort(sort).Project(projection).Limit(20).ToList();
            List<string> comments = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                comments.Add(documents[i].ToJson());
            }
            String someComments = GetJSON.GetJSON.getJsonString(comments);
            return someComments;
        }

        //获取全部评论里最新的
        public static String GetLatestComments()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("time");
            //var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            var sort = Builders<BsonDocument>.Sort.Descending("time");
            var documents = GetConnectComment.collection.Find(filter).Sort(sort).Project(projection).Limit(20).ToList();
            List<string> comments = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                comments.Add(documents[i].ToJson());
            }
            String someComments = GetJSON.GetJSON.getJsonString(comments);
            return someComments;
        }

        //提交评论
        public static String SubmitComment(String name, String title, String commentContent, String Rate)
        {
            System.DateTime currentTime = System.DateTime.Now;
            String timeNow = System.DateTime.Now.ToString();
            var document = new BsonDocument
            {
                { "user", name },
                {"film", title},
                {"content",commentContent },
                {"rate", Rate},
                {"time", currentTime},
            };
            GetConnectComment.collection.InsertOne(document);
            return "CommentOK";
        }
    }
}