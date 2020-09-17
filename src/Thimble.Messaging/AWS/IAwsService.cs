using Amazon.DynamoDBv2;
using Amazon.SimpleEmail;

namespace Thimble.Messaging.AWS
{
    public interface IAwsService
    {
        AmazonSimpleEmailServiceClient GetSesClient();
        AmazonDynamoDBClient GetDynamoClient();
    }
}