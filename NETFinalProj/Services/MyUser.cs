using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NETFinalProj.Dao;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
namespace NETFinalProj.Services
{
    public class MyUser
    {
        //添加用户
        public static String AddUser(String name,String password)
        {
            var document = new BsonDocument
            {
                { "name", name },
                {"password", password},
            };
            GetConnectUser.collection.InsertOne(document);
            return "CommentOK";
        }

        //查询用户是否输入正确用户名密码
        public static String JudgeRight(String name,String password)
        {
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter;
            filter = builder.And(builder.Eq("name", name), builder.Eq("password", password));
            //var filter = Builders<BsonDocument>.Filter.Eq("countries", country);
            //var documents = GetConnectFilm.collection.Find(filter).ToList();
            //List<string> movies = new List<string>();
            //for (int i = 0; i < documents.Count; i++)
            //{
            //    movies.Add(documents[i].ToJson());
            //}
            //String someMovie = GetJSON.getJsonString(movies);
            var documents = GetConnectFilm.collection.Find(filter).ToList();
            List<string> comments = new List<string>();
            for (int i = 0; i < documents.Count; i++)
            {
                comments.Add(documents[i].ToJson());
            }
            //String someMovie = GetJSON.getJsonString(comments);
            int count = comments.ToArray().Length;
            if (count!=0)
            { return "right"; }
            else 
            { return "wrong"; }
        }

    }
}