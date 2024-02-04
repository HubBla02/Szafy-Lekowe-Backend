using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using SzafyNaLeki.Entities;
using SzafyNaLeki.Models;
using SzafyNaLeki.Services;

namespace SzafyNaLeki.Controllers;

[Route("/api/szafa")]
public class SzafaController : ControllerBase
{

    private readonly ISzafaService _szafaService;

    public SzafaController(ISzafaService szafaService)
    {
        _szafaService = szafaService;
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        var szafy = _szafaService.GetAll();
        return Ok(szafy);
    }

    [HttpGet("{id}")]
    public ActionResult<SzafaDto> Get([FromRoute] int id)
    {
        var szafa = _szafaService.GetById(id);
        if (szafa == null)
        {
            return NotFound();
        }
        return Ok(szafa);
    }

    [HttpPost]
    public ActionResult UtworzSzafe([FromBody] UtworzSzafeDto dto)
    {
        if (dto.Temperatura1.GetType() != typeof(float) || dto.Temperatura2.GetType() != typeof(float) || dto == null)
        {
            return BadRequest("Temperatury muszą być typu float!");
        }
        var id = _szafaService.Create(dto);
        return Created($"/api/szafa/{id}", null);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        var isDeleted = _szafaService.Delete(id);
        if (isDeleted)
        {
            return NoContent();
        }
        return NotFound();

    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] int id, [FromBody] AktualizujSzafeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool isUpdated = _szafaService.Update(id, dto);
        if (!isUpdated) { return NotFound(); }
        return Ok();
    }
    [HttpGet("trigger")]
    public IActionResult TriggerJob()
    {
        try
        {
            BackgroundJob.Enqueue(() => Program.ExecuteGetAll());
            return Ok("Cykliczne zadanie uruchomione");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
        }
    }
}
