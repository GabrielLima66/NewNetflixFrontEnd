using AspNetCoreMVCNewNetflix.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AspNetCoreMVCNewNetflix.Controllers
{
    public class MovieController : Controller

    {
        string urlApi = "https://localhost:7223/Movie/";
        
        public async Task<IActionResult> Index()
        {
            

            using (var client = new HttpClient())
            {
                IEnumerable<MovieViewModel> movies;
                using (var response = await client.GetAsync(urlApi))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(apiResponse);
                    return View(movies);
                };
            }

        }
        

        public async Task<IActionResult> Create(MovieViewModel movie)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage();

            var content = ToRequest(movie);

            var response = await httpClient.PostAsync("https://localhost:7223/Movie", content);
            
            return View();
            }
        private static StringContent ToRequest(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8,"application/json");

            return data;
        }
        
        public async Task<IActionResult> Edit(MovieViewModel movie,int id)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage();

            var content = ToRequest(movie);

            var response = await httpClient.PostAsync("https://localhost:7223/Movie", content);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();

            MovieViewModel movie;
            using (var response = await httpClient.GetAsync(urlApi + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                movie = JsonConvert.DeserializeObject<MovieViewModel>(apiResponse);

                return View(movie);
            }

        }
        public async Task<IActionResult> Delete(int id, MovieViewModel movie)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync(urlApi+id);


            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = new HttpClient();

            MovieViewModel movie;
        using(var response = await httpClient.GetAsync(urlApi+id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                movie = JsonConvert.DeserializeObject<MovieViewModel>(apiResponse);

                return View(movie);
            }
                
        }
    }
}


    





