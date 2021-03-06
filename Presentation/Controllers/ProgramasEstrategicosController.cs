﻿using Application.Poa.Dto;
using Application.Poa.Services;
using System;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class ProgramasEstrategicosController : Controller
    {
        private readonly IProgramaEstrategicoService _service;

        public ProgramasEstrategicosController(IProgramaEstrategicoService service)
        {
            if (service == null) throw new ArgumentNullException("service");
            _service = service;
        }

        // GET: /ProgramasEstrategicos/
        public ActionResult Index()
        {
            var programasEstrategicos = _service.ObtenerProgramasEstrategicos();
            return View(programasEstrategicos);
        }

        // GET: /ProgramasEstrategicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ProgramasEstrategicos/Create
        [HttpPost]
        public ActionResult Create(ProgramaEstrategicoDto dto)
        {
            try
            {
                _service.CrearProgramaEstrategico(dto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: /ProgramasEstrategicos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: /ProgramasEstrategicos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var dto = new ProgramaEstrategicoDto();
                dto.Id = id;
                dto.Nombre = collection["Nombre"];
                dto.Descripcion = collection["Descripcion"];
                dto.Activo = collection["Activo"] == "on";
                _service.CrearProgramaEstrategico(dto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }   
        }

        // GET: /ProgramasEstrategicos/Delete/5
        public ActionResult Delete(int id)
        {
            _service.EliminarProgramaEstrategico(id);
            return RedirectToAction("Index");
        }

        // POST: /ProgramasEstrategicos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}