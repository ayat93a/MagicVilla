using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using MagicVilla_VillaAPI.Repositry.IRepositry;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MagicVilla_VillaAPI.Controllers
{

    [Route("/api/VillaNoAPI")]

    [ApiController]

    public class VillaNoAPIController : ControllerBase
    {
        private readonly IVillaNoRepositry _dbVillaNo;
        private readonly IVillaRepositry _dbVilla; 
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public VillaNoAPIController(IVillaNoRepositry dbVillaNo, IMapper mapper , IVillaRepositry dbVilla)
        {
            _dbVillaNo = dbVillaNo;
            _dbVilla = dbVilla; 
            _mapper = mapper;
            this._response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetVillasNo()
        {
            try
            {
                IEnumerable<VillaNumber> villaNoList = await _dbVillaNo.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaNoDTO>>(villaNoList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }



        [HttpGet("{id}", Name = "GetVillaNo")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNo(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _dbVillaNo.GetAsync(x => x.VillaNo == id); 

                if (villa == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaNoDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response; 
    }


        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> CreateVillaNo([FromBody] VillaNoCreateDTO createDTO)
        {
            try
            {

                if (await _dbVillaNo.GetAsync(u => u.VillaNo == createDTO.VillaNo) != null )
                     
                {
                    ModelState.AddModelError("", "Already Exists !");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                if (await _dbVilla.GetAsync(u => u.Id == createDTO.VillaNo) != null)
                {
                    VillaNumber villa = _mapper.Map<VillaNumber>(createDTO);
                    await _dbVillaNo.CreateAsync(villa);

                    _response.StatusCode = HttpStatusCode.Created;
                    _response.Result = _mapper.Map<VillaNoDTO>(villa);

                    return CreatedAtRoute("GetVilla", new { id = villa.VillaNo }, villa);

                }


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;    
                _response.ErrorMessage
                       = new List<string> { ex.Message };
            }
            return _response;
        }



        [HttpDelete("{id:int}" , Name = "DeleteVillaNo")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]  

        public async Task <ActionResult<APIResponse>> DeleteVillaNo(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var villa = await _dbVillaNo.GetAsync(u => u.VillaNo == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await _dbVillaNo.RemoveAsync(villa); 
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(villa);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };

            }
            return _response;
        }


        [HttpPut("{id}" , Name = "UpdateVillaNo")]
        //[HttpPut("{id}", Name = "UpdateVillaNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task <ActionResult<APIResponse>> UpdateVillaNo(int id , [FromBody] VillaNoUpdateDTO villaNoUpdate)
        {
            try
            {
                if (villaNoUpdate == null || id != villaNoUpdate.VillaNo )
                {
                    return BadRequest();
                }
                var model = await _dbVillaNo.GetAsync(u=>u.VillaNo == villaNoUpdate.VillaNo);
                if (model == null)
                {
                    return NotFound();
                }
                VillaNumber villa = _mapper.Map<VillaNumber>(villaNoUpdate);

                await _dbVillaNo.UpdateAsync(villa);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;

                return _response;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }






    }
}
