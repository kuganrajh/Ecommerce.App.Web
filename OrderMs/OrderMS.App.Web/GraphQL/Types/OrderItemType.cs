using HotChocolate.Types;
using HotChocolate.Data;
using OrderMS.App.Domain;

namespace OrderMS.App.Web.GraphQL.Types
{
    public class OrderItemType : ObjectType<OrderItem>
    {
        protected override void Configure(IObjectTypeDescriptor<OrderItem> descriptor)
        {
            descriptor.Field(f => f.ProductId).Type<StringType>();
            descriptor.Field(f => f.Name).Type<StringType>();
            descriptor.Field(f => f.Quantity).Type<IntType>();
            descriptor.Field(f => f.Price).Type<DecimalType>();

            descriptor.BindFieldsExplicitly();
        }
    }
}


