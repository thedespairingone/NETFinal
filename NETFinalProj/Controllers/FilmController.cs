using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NETFinalProj.Services;

namespace NETFinalProj.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        public ActionResult FilmIndex(String input="")
        {
            Console.WriteLine(input + "Controller");
            ViewBag.SearchKey = input;
            ViewBag.Message = "Your contact page.";
            ViewBag.movie = Film.Search(input);
            ViewBag.movieRank15 = "";
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
            return View();
        }

        //sortMethod为0按照热度排序，为1按照评分排序
        public ActionResult FilmAll(String country="", String language = "", String genre = "", int skip=0, int sortMethod=0)
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
            return View();
        }

        public ActionResult FilmDetail(String input = "")
        {
            Console.WriteLine(input + "Controller");
            ViewBag.SearchKey = input;
            ViewBag.Message = "Your contact page.";
            ViewBag.movie = Film.Search(input);
            return View();
        }

        public ActionResult FilmSearch(String input = "")
        {
            Console.WriteLine(input + "Controller");
            ViewBag.SearchKey = input;
            ViewBag.Message = "FilmSearch page.";
            ViewBag.movieList = Film.SearchAll(input);
            return View();
        }

        public ActionResult AddFilm()
        {
            return View();
        }
        public ActionResult AddFilmOver(String title, String poster,String directors, String writers, String casts, String genres, String countries,
        String languages, String pubdate, String length, String summary)
        {
            Film.InsertFilm( title,  poster, directors,  writers,  casts,  genres,  countries,
         languages,  pubdate,  length,  summary);
            ViewBag.SearchKey = title;
            ViewBag.Message = "Your contact page.";
             return Redirect("FilmIndex"); 
        }

        public ActionResult UpdateFilm(String title)
        {
            ViewBag.movie = Film.Search(title);
            return View();
        }

        public ActionResult UpdateFilmOver(String title, String poster, String directors, String writers, String casts, String genres, String countries,
        String languages, String pubdate, String length, String summary)
        {
            Film.UpdateFilm(title, poster, directors, writers, casts, genres, countries,
         languages, pubdate, length, summary);
            ViewBag.SearchKey = title;
            ViewBag.Message = "Your contact page.";
            return Redirect("FilmIndex");
        }

        public ActionResult DeleteFilm(String title)
        {
            Film.DeleteFilm(title);
            ViewBag.Message = "Your contact page.";
            return Redirect("FilmIndex"); 
        }

        public ActionResult TestStar()
        { 
            return View();
        }

    }
}