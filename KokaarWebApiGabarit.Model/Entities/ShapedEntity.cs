using System;
using System.Collections.Generic;
using System.Text;

namespace KokaarWebApiGabarit.Model.Entities
{

    public class ShapedEntity
    {
        public ShapedEntity()
        {
            Entity = new Entity();
        }
        public int Id { get; set; }
        public Entity Entity { get; set; }
    }

}
