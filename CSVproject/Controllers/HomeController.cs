using CSVproject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CsvHelper;
using System.Globalization;
using System.Formats.Asn1;
using CsvHelper.Configuration;

namespace CSVproject.Controllers
{
    public class HomeController : Controller
    {

        private static List<Person> _people = new List<Person>();

        public IActionResult Index(string sortOrder)
        {
            ViewBag.FirstNameSortParam = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewBag.DateOfBirthSortParam = sortOrder == "dateOfBirth" ? "dateOfBirth_desc" : "dateOfBirth";
            switch (sortOrder)
            {
                case "firstName":
                    _people = _people.OrderBy(p => p.FirstName).ToList();
                    break;
                case "firstName_desc":
                    _people = _people.OrderByDescending(p => p.FirstName).ToList();
                    break;
                case "dateOfBirth":
                    _people = _people.OrderBy(p => p.DateTime).ToList();
                    break;
                case "dateOfBirth_desc":
                    _people = _people.OrderByDescending(p => p.DateTime).ToList();
                    break;
            }
            return View(_people);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ","
            };

            if (file == null || file.Length == 0)
                return BadRequest("Файл не загружен!");

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csvReader = new CsvHelper.CsvReader(reader, config))
            {
                try
                {
                    var records = csvReader.GetRecords<Person>();
                    _people.AddRange(records);
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError("file", "Error parsing CSV file: " + ex.Message);
                    //ViewBag.Message = "Record Inserted successfully";
                    //return BadRequest($"Error: {ex.Message}");
                    return BadRequest("Файл не соответствует стадарту!");

                }

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetPeople()
        {
            return Json(_people);
        }
    }
}