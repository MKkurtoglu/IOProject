using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Kullanıcının sepetindeki tüm ürünleri getirir
        /// </summary>
        [HttpGet("items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CartDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCartItemsAsync()
        {
            var result = await _cartService.GetCartItemsAsync();

            if (!result.IsSuccess)
                return NotFound(new { message = result.Message });

            return Ok(new
            {
                success = true,
                data = result.Data,
                message = result.Message
            });
        }

        /// <summary>
        /// Sepetin toplam tutarını hesaplar
        /// </summary>
        [HttpGet("total")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartTotalAsync()
        {
            var result = await _cartService.GetCartTotalAsync();

            return Ok(new
            {
                success = true,
                total = result.Data,
                message = result.Message
            });
        }

        /// <summary>
        /// Sepete yeni bir ürün ekler
        /// </summary>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Quantity <= 0)
                return BadRequest(new { message = "Miktar 0'dan büyük olmalıdır." });

            var cartItem = new Cart
            {
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!),
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            var result = await _cartService.AddAsync(cartItem);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Created($"api/cart/items", new
            {
                success = true,
                message = result.Message
            });
        }

        /// <summary>
        /// Sepetteki bir ürünün miktarını günceller
        /// </summary>
        [HttpPut("update-quantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateQuantityAsync(int cartId, [FromBody] UpdateQuantityRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartService.UpdateQuantityAsync(cartId, request.Quantity);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new
            {
                success = true,
                message = result.Message
            });
        }

        /// <summary>
        /// Sepetten bir ürünü siler
        /// </summary>
        [HttpPost("remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFromCartAsync(int productId)
        {
            var cartItem = await _cartService.GetByIdAsync(productId);

            if (!cartItem.IsSuccess)
                return NotFound(new { message = "Sepet öğesi bulunamadı." });

            // Kullanıcının kendi sepet öğesini sildiğinden emin oluyoruz
            if (cartItem.Data.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!))
                return Forbid();

            var result = await _cartService.DeleteAsync(cartItem.Data);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new
            {
                success = true,
                message = result.Message
            });
        }

        /// <summary>
        /// Sepeti tamamen temizler
        /// </summary>
        [HttpPost("clear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ClearCartAsync()
        {
            var cartItems = await _cartService.GetAllAsync();

            if (cartItems.IsSuccess && cartItems.Data.Any())
            {
                foreach (var item in cartItems.Data)
                {
                    await _cartService.DeleteAsync(item);
                }
            }

            return Ok(new
            {
                success = true,
                message = "Sepet temizlendi."
            });
        }
    }

   

    public record AddToCartRequest(int ProductId, int Quantity);
    public record UpdateQuantityRequest(int Quantity);
}