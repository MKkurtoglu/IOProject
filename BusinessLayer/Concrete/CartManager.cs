using Base.Extensions;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartDal _cartDal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartManager(ICartDal cartDal, IHttpContextAccessor httpContextAccessor)
        {
            _cartDal = cartDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> DeleteAsync(Cart entity)
        {
            if (entity != null)
            {
                await _cartDal.DeleteAsync(entity);
                return new SuccessResult("Ürün sepetten kaldırıldı.");
            }
            return new ErrorResult("Silme işlemi başarısız.");
        }

        public async Task<IDataResult<Cart>> GetAsync(int id)
        {
            var user = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(user);
            var result = await _cartDal.GetAsync(c => c.ProductId == id && c.UserId == userId);
            return result == null
                ? new ErrorDataResult<Cart>(result, "Sepet öğesi bulunamadı.")
                : new SuccessDataResult<Cart>(result);
        }

        public async Task<IDataResult<List<Cart>>> GetAllAsync()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var result = await _cartDal.GetAllAsync(c => c.UserId == userId);
            return result != null
                ? new SuccessDataResult<List<Cart>>(result)
                : new ErrorDataResult<List<Cart>>();
        }

        public async Task<IResult> AddAsync(Cart entity)
        {
            var existingItem = await _cartDal.GetAsync(c => c.UserId == entity.UserId && c.ProductId == entity.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += entity.Quantity;
                return await UpdateAsync(existingItem);
            }

            if (entity != null)
            {
                await _cartDal.AddAsync(entity);
                return new SuccessResult("Ürün sepete eklendi.");
            }
            return new ErrorResult("Ekleme işlemi başarısız.");
        }

        public async Task<IResult> UpdateAsync(Cart entity)
        {
            if (entity != null)
            {
                await _cartDal.UpdateAsync(entity);
                return new SuccessResult("Sepet güncellendi.");
            }
            return new ErrorResult("Güncelleme işlemi başarısız.");
        }

        public async Task<IDataResult<List<CartDto>>> GetCartItemsAsync()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var data = await _cartDal.GetAllCartItemsByUserAsync(userId);
            return data == null
                ? new ErrorDataResult<List<CartDto>>(new List<CartDto>(), "Sepet boş.")
                : new SuccessDataResult<List<CartDto>>(data);
        }

        public async Task<IDataResult<decimal>> GetCartTotalAsync()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var total = await _cartDal.GetCartTotalAsync(userId);
            return new SuccessDataResult<decimal>(total);
        }

        public async Task<IResult> UpdateQuantityAsync(int cartId, int quantity)
        {
            if (quantity < 1)
            {
                return new ErrorResult("Miktar 1'den küçük olamaz.");
            }

            var cartItem = await _cartDal.GetAsync(c => c.CartId == cartId);
            if (cartItem == null)
            {
                return new ErrorResult("Sepet öğesi bulunamadı.");
            }

            cartItem.Quantity = quantity;
            await _cartDal.UpdateAsync(cartItem);
            return new SuccessResult("Miktar güncellendi.");
        }

        public async Task<IDataResult<Cart>> GetByIdAsync(int id)
        {
            var user = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(user);
            var result = await _cartDal.GetAsync(c => c.ProductId == id && c.UserId == userId);
            return result == null
                ? new ErrorDataResult<Cart>(result, "Sepet öğesi bulunamadı.")
                : new SuccessDataResult<Cart>(result);
        }
    }
}
