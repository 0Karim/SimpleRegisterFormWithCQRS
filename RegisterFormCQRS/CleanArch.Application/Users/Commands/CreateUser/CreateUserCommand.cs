using CleanArch.Application.Common.Interfaces;
using CleanArch.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<long>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public List<AddressInfoCommand> AddressInfos { set; get; }
    }

    public class AddressInfoCommand
    {
        public int GovId { get; set; }

        public int CityId { get; set; }

        public int UserId { get; set; }

        public string BuildingNumber { get; set; }

        public string Street { get; set; }

        public string FlatNumber { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IDbContext _context;
        private readonly IDateTime _dateTime;

        public CreateUserCommandHandler(IDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    MobileNumber = request.MobileNumber
                };

                var addresslist = new List<AddressInfo>();
                _context.User.Add(user);
                var success = await _context.SaveChangesAsync(cancellationToken);

                int currentUserId = user.Id;

                foreach(var addressInfo in request.AddressInfos)
                {
                    var address = new AddressInfo
                    {
                        CityId = addressInfo.CityId,
                        GovId = addressInfo.GovId,
                        UserId = currentUserId,
                        BuildingNumber = addressInfo.BuildingNumber,
                        FlatNumber = addressInfo.FlatNumber
                    };

                    addresslist.Add(address);
                }

                _context.AddressInfo.AddRange(addresslist);
                await _context.SaveChangesAsync(cancellationToken);

                return success;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
