﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NETFinalProj.Services;
using System.Threading;

namespace NETFinalProj.Controllers
{
    public class FilmController : Controller
    {
        static int Times = 0;
        private static readonly object o = new object();
        public static void add()
        {
            lock (o)
            {
                Times=Times+1;
            }
        }
        // GET: Film
        public ActionResult FilmIndex(String name=null)
        {
            Thread t=new Thread(FilmController.add);
            t.Start();
            ViewBag.Message = "Your contact page.";
            
            //热门电影
            ViewBag.hotMovie = Film.getSortedMovieListByHot();
            ViewBag.ChineseHotMovie = Film.getSortedMovieListByHot("中国");
            ViewBag.AmericanHotMovie = Film.getSortedMovieListByHot("美国");
            //高评分电影
            ViewBag.highRateMovie = Film.getSortedMovieListByRate();
            ViewBag.ChineseHighRateMovie = Film.getSortedMovieListByRate("中国");
            ViewBag.AmericanHighRateMovie = Film.getSortedMovieListByRate("美国");
            ViewBag.EnglishHighRateMovie = Film.getSortedMovieListByRate("英国");
            ViewBag.ItlianHighRateMovie = Film.getSortedMovieListByRate("意大利");
            ViewBag.KoreanHighRateMovie = Film.getSortedMovieListByRate("韩国");
            ViewBag.JapanHighRateMovie = Film.getSortedMovieListByRate("日本");
            ViewBag.times = FilmController.Times;
            //最新评论
            ViewBag.newestComments = Comment.GetLatestComments();
            ViewBag.name = name;
            return View();
        }

        //sortMethod为0按照热度排序，为1按照评分排序
        public ActionResult FilmAll(String name=null,String country="全部", String language = "全部", String genre = "全部", int skip=0, int sortMethod=0)
        {
            ViewBag.Message = "FilmSearch page.";
            if (sortMethod == 0)
            {
                ViewBag.movies = Film.getAllFilmSortByHot(country, language, genre, skip);
            }
            else if (sortMethod == 1)
            {
                ViewBag.movies = Film.getAllFilmSortByRate(country, language, genre, skip);
            }
            ViewBag.country = country;
            ViewBag.language = language;
            ViewBag.skip = skip;
            ViewBag.sortMethod = sortMethod;
            ViewBag.genre = genre;
            ViewBag.skip = skip;
            ViewBag.name = name;
            return View();
        }

        public ActionResult FilmDetail(String input = "",String name=null)
        {
            Console.WriteLine(input + "Controller");
            ViewBag.SearchKey = input;
            ViewBag.Message = "Your contact page.";
            ViewBag.movie = Film.Search(input);
            ViewBag.newestComments = Comment.GetLatestComments(input);
            ViewBag.name =name;
            return View();
        }

        public ActionResult FilmSearch(String input = "",String name=null)
        {
            Console.WriteLine(input + "Controller");
            ViewBag.SearchKey = input;
            ViewBag.Message = "FilmSearch page.";
            ViewBag.movieList = Film.SearchAll(input);
            ViewBag.searchCt = input;
            ViewBag.name = name;
            return View();
        }

        public ActionResult AddFilm(String name=null)
        {
            ViewBag.name = name;
            return View();
        }
        public ActionResult AddFilmOver(String title, String poster,String directors, String writers, String casts, String genres, String countries,
        String languages, String pubdate, String length, String summary,String name=null)
        {
            Film.InsertFilm( title,  poster, directors,  writers,  casts,  genres,  countries,
         languages,  pubdate,  length,  summary);
            ViewBag.SearchKey = title;
            ViewBag.Message = "Your contact page.";
             return Redirect("FilmIndex?name="+name); 
        }

        public ActionResult UpdateFilm(String title,String name=null)
        {
            ViewBag.name = name;
            ViewBag.movie = Film.Search(title);
            return View();
        }

        public ActionResult UpdateFilmOver(String title, String poster, String directors, String writers, String casts, String genres, String countries,
        String languages, String pubdate, String length, String summary,String name=null)
        {
            Film.UpdateFilm(title, poster, directors, writers, casts, genres, countries,
         languages, pubdate, length, summary);
            ViewBag.SearchKey = title;
            ViewBag.Message = "Your contact page.";
            ViewBag.name = name;
            return Redirect("FilmIndex?name="+name);
        }

        public ActionResult DeleteFilm(String title,String name=null)
        {
            Film.DeleteFilm(title);
            ViewBag.Message = "Your contact page.";
            ViewBag.name = name;
            return Redirect("FilmIndex?name="+name); 
        }

        //提交评价
        public ActionResult FilmDetailCommentOver(String name,String title,String commentContent,String Rate)
        {
            Comment.SubmitComment(name,title,commentContent,Rate);
            Film.SubmitRate(title, Rate);
            ViewBag.movie = Film.Search(title);
            return Redirect("FilmDetail?input=" + title+"&name="+name);
        }

        public ActionResult TestStar()
        { 
            return View();
        }

        public JsonResult SignIn(string name, string password)
        {
            var judge = "-1";
            if (MyUser.JudgeRight(name,password)== "right")
            {
                judge = "1";
            }
            return Json(judge, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SignUp(string name, string password)
        {
            var judge = "1";
            MyUser.AddUser(name, password);
            return Json(judge, JsonRequestBehavior.AllowGet);
        }

    }
}