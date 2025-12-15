using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientRepository.GetAllAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
        {
            var patient = new Patient
            {
                PatientId = Guid.NewGuid(),
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Address = dto.Address,
                CreatedOn = DateTime.UtcNow,
                IsActive = true
            };

            await _patientRepository.AddAsync(patient);
            return CreatedAtAction(nameof(GetById), new { id = patient.PatientId }, patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientDto dto)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();

            patient.FirstName = dto.FirstName ?? patient.FirstName;
            patient.MiddleName = dto.MiddleName ?? patient.MiddleName;
            patient.LastName = dto.LastName ?? patient.LastName;
            patient.Gender = dto.Gender ?? patient.Gender;
            patient.DateOfBirth = dto.DateOfBirth ?? patient.DateOfBirth;
            patient.PhoneNumber = dto.PhoneNumber ?? patient.PhoneNumber;
            patient.Email = dto.Email ?? patient.Email;
            patient.Address = dto.Address ?? patient.Address;
            if (dto.IsActive.HasValue)
                patient.IsActive = dto.IsActive.Value;

            patient.ModifiedOn = DateTime.UtcNow;

            await _patientRepository.UpdateAsync(patient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();

            await _patientRepository.DeleteAsync(patient);
            return NoContent();
        }
    }
}
