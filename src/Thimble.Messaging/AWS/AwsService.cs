using Amazon;
using Amazon.DynamoDBv2;
using Amazon.SimpleEmail;
using Microsoft.Extensions.Configuration;

namespace Thimble.Messaging.AWS
{
    public class AwsService : IAwsService
    {
        private IConfiguration Configuration;
        public AwsService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public AmazonSimpleEmailServiceClient GetSesClient()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(Configuration["awsAccessKey"], Configuration["awsSecretKey"]); 
            return new AmazonSimpleEmailServiceClient(awsCredentials, RegionEndpoint.EUWest1);;
        }

        public AmazonDynamoDBClient GetDynamoClient()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(Configuration["awsAccessKey"], Configuration["awsSecretKey"]); 
            return new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.EUWest1);
        }
    }
}