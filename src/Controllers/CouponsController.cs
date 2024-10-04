using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Controller;
using src.Services.Payment;
using src.Repository;
using static src.DTO.CouponDTO;
using Microsoft.AspNetCore.Authorization;
using src.Services.Coupon;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponsController : ControllerBase
    {
        protected readonly ICouponService _couponService;
        public CouponsController(ICouponService service)
        {
            _couponService = service;
        }
        
        // Get all coupon: GET api/v1/coupons
        [HttpGet]
        public async Task<ActionResult<List<CouponReadDto>>> GetAllCoupons()
        {
            var coupon_list = await _couponService.GetAllAsync();
            return Ok(coupon_list);
        }
    
    // get coupon by id: GET api/v1/coupons/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponReadDto>> GetCouponById(Guid id)
        {
            var coupon = await _couponService.GetByIdAsync(id);
            return Ok(coupon);
        }
    
    
    // create coupon: POST api/v1/coupons
    [HttpPost]
    public async Task<ActionResult<CouponReadDto>> CreateCoupon(CouponCreateDto coupon){

        var created_coupon = await _couponService.CreateOneAsync(coupon);

        return Ok(created_coupon);
    }
    
    // update coupon: PUT api/v1/coupons/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<CouponReadDto>> UpdateCoupon(Guid id, CouponUpdateDto coupon){

        var updated_coupon = await _couponService.UpdateOneAsync(id,coupon);

        return Ok(updated_coupon);
    }
    // delete coupon: DELETE api/v1/coupons/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOneAsync([FromRoute] Guid id)
    {
        var result = await _couponService.DeleteOneAsync(id);
        if (!result)
        {
            return NotFound($"Coupon with ID = {id} not found.");
        }
        return NoContent(); // 204 No Content
        }
    }
}
      