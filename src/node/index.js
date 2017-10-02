// Load http client module
var request = require('request-promise');

module.exports = function (context, req) {


    if (req.body) {
        

            // Pass the image to Azure Cognitive Services API to retrieve image data
            var options = {
                uri: "https://westus2.api.cognitive.microsoft.com/vision/v1.0/analyze?visualfeatures=tags,description",
                method: 'POST',
                json: {
                "url": req.body
                },
                headers: {
                    'Content-Type': 'application/json',
                    'Ocp-Apim-Subscription-Key': process.env.Vision_API_Subscription_Key
                }
            };
            context.log("Calling Cog Svcs....");
            request(options)
                .then((response) => {

                    var respMsg = "Not Hot Dog";
                    for(i = 0; i < response.tags.length; i++){
                        if(response.tags[i].name == "hotdog"){
                            respMsg = "Hot Dog";
                        };
                    };
                
                        context.res = {
                        status: 200,
                        body: respMsg
                   };
            })
            .catch((error) => context.log(error))
            .finally(() => context.done());
    }
    else {
        context.res = {
            status: 400,
            body: "Please pass an image URL in the request body"
        };
        context.done();
    }
};