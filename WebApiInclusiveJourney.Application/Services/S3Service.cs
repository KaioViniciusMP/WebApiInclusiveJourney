using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Application.Services
{
    public class S3Service
    {

        private readonly string _bucketName;

        [Required]
        private readonly string _directoryName;


        private readonly IAmazonS3 _client;
        private readonly string _awsAccessKey = "";
        private readonly string _awsSecretKey = "";

        public S3Service(string diretory, string bucket)
        {
            _directoryName = diretory;
            _bucketName = bucket;

            _client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.USEast1);
        }

        public S3Service(string diretory)
        {
            _directoryName = diretory;

            _bucketName = "kainho";

            _client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.USEast1);
        }
        public void Upload(System.IO.Stream localFilePath, string fileNameInS3)
        {
            TransferUtility utility = new TransferUtility(_client);
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (_directoryName == "" || _directoryName == null)
                request.Key = fileNameInS3;
            else
                request.Key = $"{_directoryName}/{fileNameInS3}";

            request.BucketName = _bucketName;
            request.InputStream = localFilePath;
            request.ContentType = "image/jpeg";
            request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256;

            try
            {
                utility.Upload(request);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    throw new Exception("Check the provided AWS Credentials.");
                else
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
            }
        }
        public string GetUrlFile(string fileName, double duration)
        {
            string urlString = string.Empty;
            try
            {
                string pathFile = $"{_directoryName}/{fileName}";

                if (FileExists(pathFile))
                {
                    var request = new GetPreSignedUrlRequest()
                    {
                        BucketName = _bucketName,
                        Key = $"{_directoryName}/{fileName}",
                        Expires = DateTime.UtcNow.AddHours(duration),
                    };
                    urlString = _client.GetPreSignedURL(request);
                }
            }
            catch (AmazonS3Exception ex)
            {
                throw new Exception("Error occurred: " + ex.Message);
            }

            return urlString;
        }

        public bool FileExists(string filePath)
        {
            try
            {
                var returno = _client.GetObjectMetadataAsync(_bucketName, filePath).Result;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
