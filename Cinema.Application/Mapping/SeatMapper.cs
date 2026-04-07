using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class SeatMapper
    {
        public Inventary CreateSeatEntity(
            int hallId, 
            int row, 
            int number, 
            ConditionStatus type, 
            float coefficient)
        {
            return new Inventary
            {
                HallId = hallId,
                RowNumber = (byte)row,
                SeatNumber = (byte)number,
                Type = type,
                Coefficient = coefficient
            };
        }
    }
}
