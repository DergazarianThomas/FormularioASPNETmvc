using FormularioASPNET.Data;
using Microsoft.AspNetCore.Mvc;
using FormularioASPNET.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FormularioASPNET.DTOs;


namespace FormularioASPNET.Controllers
{

    public class AsistenciaController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public AsistenciaController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var asistencias = await _appDbContext.Asistencias
                .Include(a => a.Alumno)
                .ToListAsync();
            return View(asistencias);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            ViewData["Alumnos"] = new SelectList(_appDbContext.Alumnos, "IdAlumno", "NombreAlumno");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo([FromForm] AsistenciaCreacionDTO asistenciaCreacionDTO)
        {
            if (ModelState.IsValid)
            {
                Asistencia nuevaAsistencia = new Asistencia();
                {
                    nuevaAsistencia.IdAlumno = asistenciaCreacionDTO.IdAlumno;
                    nuevaAsistencia.Date = asistenciaCreacionDTO.Date;
                    nuevaAsistencia.Presente = asistenciaCreacionDTO.Presente;
                };

                await _appDbContext.Asistencias.AddAsync(nuevaAsistencia);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Lista));
            }
            ViewData["Alumnos"] = new SelectList(_appDbContext.Alumnos, "IdAlumno", "NombreAlumno");
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var asistencia = await _appDbContext.Asistencias
                .Include(a => a.Alumno)
                .FirstOrDefaultAsync(a => a.IdAsistencia == id);
            if (asistencia == null)
            {
                return NotFound();
            }
            ViewData["Alumnos"] = new SelectList(_appDbContext.Alumnos, "IdAlumno", "NombreAlumno", asistencia.IdAlumno);
            return View(asistencia);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Asistencia asistencia)
        {

            
            _appDbContext.Asistencias.Update(asistencia);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));


        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Asistencia asistencia = await _appDbContext.Asistencias.FirstAsync(e => e.IdAsistencia == id);

            _appDbContext.Asistencias.Remove(asistencia);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

    }
}
