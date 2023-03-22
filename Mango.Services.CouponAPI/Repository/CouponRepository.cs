﻿using AutoMapper;
using Mango.Services.CouponAPI.DbContexts;
using Mango.Services.CouponAPI.Migrations;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Repository;

public class CouponRepository : ICouponRepository
{

    private readonly ApplicationDbContext _db;
    protected IMapper _mapper;

    public CouponRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<CouponDTO> GetCouponByCode(string couponCode)
    {
        var couponFromDb = await _db.coupons.FirstOrDefaultAsync(u => u.CouponCode == couponCode);
        return _mapper.Map<CouponDTO>(couponFromDb);
    }
}
