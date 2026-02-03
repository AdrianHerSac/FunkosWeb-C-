using System.Diagnostics;
using FunkoWeb.Data;
using Microsoft.AspNetCore.Mvc;
using FunkoWeb.Models;
using FunkoWeb.DTOs;
using FunkoWeb.Mappers;

namespace FunkoWeb.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var funkos = InMemoryData.Funkos;
        
        return View(funkos);
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

    [HttpGet]
    public IActionResult Crear()
    {
        ViewBag.Categories = InMemoryData.Categories;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Crear(FunkoCreateDto nuevoFunkoDto, IFormFile? fichero)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = InMemoryData.Categories;
            return View(nuevoFunkoDto);
        }

        // Manejar la subida de archivo
        if (fichero != null && fichero.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fichero.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fichero.CopyToAsync(fileStream);
            }

            nuevoFunkoDto.Image = "/images/" + uniqueFileName;
        }

        // Buscar la categoría
        var category = InMemoryData.Categories.FirstOrDefault(c => c.Id == nuevoFunkoDto.CategoryId);
        if (category == null)
        {
            ModelState.AddModelError("CategoryId", "La categoría seleccionada no existe");
            ViewBag.Categories = InMemoryData.Categories;
            return View(nuevoFunkoDto);
        }

        // Usar el mapper para crear la entidad
        var nuevoFunko = nuevoFunkoDto.ToEntity(category);
        InMemoryData.Funkos.Add(nuevoFunko);
    
        TempData["Mensaje"] = $"Funko {nuevoFunko.Name} creado con éxito";
        TempData["Tipo"] = "success";
    
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Modificar(Guid id)
    {
        var funko = InMemoryData.Funkos.FirstOrDefault(f => f.Id == id);
        if (funko == null) return NotFound();

        ViewBag.Categories = InMemoryData.Categories;
        
        // Convertir la entidad a DTO para edición
        var funkoDto = new FunkoUpdateDto
        {
            Id = funko.Id,
            Name = funko.Name,
            Price = funko.Price,
            Stock = funko.Stock,
            CategoryId = funko.Category.Id,
            Image = funko.Image
        };
        
        return View(funkoDto); 
    }

    [HttpPost] 
    public async Task<IActionResult> Modificar(FunkoUpdateDto funkoEditadoDto, IFormFile? fichero)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = InMemoryData.Categories;
            return View(funkoEditadoDto);
        }

        var funko = InMemoryData.Funkos.FirstOrDefault(f => f.Id == funkoEditadoDto.Id);
        
        if (funko == null)
        {
            return NotFound();
        }

        // Manejar la subida de archivo
        if (fichero != null && fichero.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fichero.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fichero.CopyToAsync(fileStream);
            }

            funkoEditadoDto.Image = "/images/" + uniqueFileName;
        }

        // Buscar la categoría
        var category = InMemoryData.Categories.FirstOrDefault(c => c.Id == funkoEditadoDto.CategoryId);
        if (category == null)
        {
            ModelState.AddModelError("CategoryId", "La categoría seleccionada no existe");
            ViewBag.Categories = InMemoryData.Categories;
            return View(funkoEditadoDto);
        }

        funko.UpdateEntity(funkoEditadoDto, category);
        
        TempData["Mensaje"] = $"Funko {funko.Name} actualizado con éxito";
        TempData["Tipo"] = "success";
        
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        var funko = InMemoryData.Funkos.FirstOrDefault(f => f.Id == id);
        if (funko != null)
        {
            InMemoryData.Funkos.Remove(funko);
        }
        TempData["Mensaje"] = "Funko eliminado correctamente";
        TempData["Tipo"] = "success";
        
        return RedirectToAction(nameof(Index));
    }
    
    // 404 por si no lo veo
    public IActionResult PaginaNoEncontrada()
    {
        return View("404"); 
    }
    
    public IActionResult ErrorDelServidor()
    {
        return View("500"); 
    }
    
    // Dark
    [HttpPost]
    public IActionResult CambiarTema(string tema, string returnUrl)
    {
        var options = new CookieOptions { 
            Expires = DateTimeOffset.UtcNow.AddYears(1), 
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };
    
        Response.Cookies.Append("User_Theme", tema, options);
    
        return LocalRedirect(returnUrl ?? "/");
    }
}