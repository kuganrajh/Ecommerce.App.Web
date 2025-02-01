using OrderMS.App.Domain;
using HotChocolate.Types;
using HotChocolate.Data;

namespace OrderMS.App.Web.GraphQL.Types
{
    public class OrderType : ObjectType<Order>
    {
        protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
        {
            descriptor.Field(f => f.Id).Type<UuidType>();
            descriptor.Field(f => f.CustomerId).Type<StringType>();
            descriptor.Field(f => f.Status).Type<StringType>();
            descriptor.Field(f => f.TotalAmount).Type<DecimalType>();

            // Enable filtering on the 'items' field
            descriptor.Field(f => f.Items)
                  .Type<ListType<OrderItemType>>()
                  .UseFiltering(); // Apply filtering here
        }
    }
}
