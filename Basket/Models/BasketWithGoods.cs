using System.Collections.Generic;

namespace Basket.Models
{
    public class BasketWithGoods
    {
        public int BasketId { get; set; }

        public int CustomerId { get; set; }

        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
