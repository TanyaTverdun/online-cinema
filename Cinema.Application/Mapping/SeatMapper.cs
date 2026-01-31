using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class SeatMapper
    {
        public Seat CreateSeatEntity(int hallId, int row, int number)
        {
            return new Seat
            {
                HallId = hallId,
                RowNumber = (byte)row,
                SeatNumber = (byte)number,
                Type = SeatType.Standard,
                Coefficient = 1.0f
            };
        }
    }
}
