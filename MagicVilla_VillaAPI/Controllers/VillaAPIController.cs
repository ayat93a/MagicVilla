using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;

namespace MagicVilla_VillaAPI.Controllers
{
    //By using the controller name
    [Route("/api/VillaAPI")]

    //by using [controller] it will call the controller class by deafult ||| its not the best practice 
    //[Route("/api/[controller]")]

    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //add verb 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        //create end point data location and make it ienumerable object 
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
            // villaStore to make it read from on-the-fly db
        }



        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // [ProducesResponseType(200 , Type = typeof(VillaDTO)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);

        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {

            if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Already Exists !");
                return BadRequest(ModelState);
                //validation errors that need to be returned to the client.
                /*By passing ModelState to the BadRequest method, you are returning an HTTP 400 Bad Request
                response to the client, along with a list of validation errors contained in the ModelState object.*/
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            VillaStore.villaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }







        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteVilla(int id)
        // ActionResult determine the type of return or what to return
        // IActionResult there is nothing to return 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }



        [HttpPut("{id}" , Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla (int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO== null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            villa.Name = villaDTO.Name;
            villa.sqft= villaDTO.sqft;
            villa.Location= villaDTO.Location;

            return NoContent();
        }



        [HttpPatch("id" , Name = "PatchVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult PatchVilla (int id , JsonPatchDocument<VillaDTO> patchDto ) //called it patchDto
            //patchDto is a json patch document
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(villa , ModelState);
            // scince it an json patch document we can use apply to and apply the things in that in villa object
            //we use Model state to noyify that if there is and edit apply that to the object 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();


        }



    }
}
