using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(ApplicationContext db, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _db = db;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result> Create(UserDtoCreation item, CancellationToken ct = default)
        {
            var userCheck = await _db.Users.FirstOrDefaultAsync(u => u.Login == item.Login, ct);
            if (userCheck == null)
            {
                var user = _mapper.Map<User>(item);
                user.PasswordHash = _passwordHasher.HashPassword(user, item.Password);
                await _db.Users.AddAsync(user, ct);
                return await _db.SaveChangesAsync(ct) > 0 
                    ? Result.Success(item) 
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("User with this login already exist.");
        }

        public async Task<Result> Delete(string login, string password, CancellationToken ct = default)
        {
            var userCheck=await _db.Users.FirstOrDefaultAsync(u=>u.Login == login, ct);
            if (userCheck != null)
            {
                var passwordCheck = _passwordHasher.VerifyHashedPassword(userCheck, userCheck.PasswordHash, password);
                if (passwordCheck == PasswordVerificationResult.Success)
                {
                    userCheck.IsDeleted = true;
                    _db.Users.Update(userCheck);
                    return await _db.SaveChangesAsync(ct) > 0
                        ? Result.Success()
                        : Result.Failure("Cannot save changes to database");
                }
                else return Result.Failure("Incorrect password.");
            }
            else return Result.Failure("User was not found");
        }

        public async Task<Result<UserDto>> Get(string login, CancellationToken ct = default)
        {
            var userCheck = await _db.Users.FirstOrDefaultAsync(u => u.Login == login, ct);
            return userCheck != null
                ? Result<UserDto>.Success(_mapper.Map<UserDto>(userCheck)) 
                : Result<UserDto>.Failure("User was not found.");
        }

        public async Task<Result<PagedResult<UserDto>>> GetAll(int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            var totalCount = await _db.Users.CountAsync(ct);

            var list = await _db.Users
                .OrderBy(u => u.Login)
                .Include(u => u.Sales)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var dtoList = _mapper.Map<IEnumerable<UserDto>>(list);

            var pageResult = new PagedResult<UserDto>()
            {
                Items=dtoList,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount/(double)pageSize)
            };

            return Result<PagedResult<UserDto>>.Success(pageResult);
        }

        public Task<Result> Update(UserDto item, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
