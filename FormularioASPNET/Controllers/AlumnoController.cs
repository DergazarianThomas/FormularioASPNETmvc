using Microsoft.AspNetCore.Mvc;
using FormularioASPNET.Data;
using FormularioASPNET.Models;
using Microsoft.EntityFrameworkCore;
using FormularioASPNET.DTOs;
using System;


namespace FormularioASPNET.Controllers
{


    public class AlumnoController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public AlumnoController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Alumno> lista = await _appDbContext.Alumnos.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo( AlumnoCreacionDTO alumnoCreacionDTO)
        {
            Alumno nuevoAlumno = new Alumno();

            nuevoAlumno.NombreAlumno = alumnoCreacionDTO.NombreAlumno;
            nuevoAlumno.Documento = alumnoCreacionDTO.Documento;
            nuevoAlumno.Nacimiento = alumnoCreacionDTO.Nacimiento;
            nuevoAlumno.Activo = alumnoCreacionDTO.Activo;


            await _appDbContext.Alumnos.AddAsync(nuevoAlumno);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Alumno alumno = await _appDbContext.Alumnos.FirstAsync(e => e.IdAlumno == id);
            return View(alumno);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Alumno alumno)
        {
            
            _appDbContext.Alumnos.Update(alumno);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Alumno alumno = await _appDbContext.Alumnos.FirstAsync(e => e.IdAlumno == id);

            _appDbContext.Alumnos.Remove(alumno);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
