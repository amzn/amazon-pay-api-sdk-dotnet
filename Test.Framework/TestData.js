[
    /**
     * This is a comprehensive list of test cases for how to generated a
     * string to sign.
     */
    {
      // A vanilla GET
      "name" : "get-vanilla",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "GET",
      "parameters" : {},
      "payload" : "",
      "canonicalRequest" : "GET\n/\n\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n13fdf6db844bdfb9e9c0e27a4251ca04e60c29ca2132249c5dd1cb09c26e22f5"
    },

    {
      // A URI with redundant slashes
      "name" : "get-slashes",
      "uri" : "http://pay-api.amazon.eu///foo//",
      "method" : "GET",
      "parameters" : {},
      "payload" : "",
      "canonicalRequest" : "GET\n/foo/\n\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\nd8e13e1857bc9b5056cc8ccb2699812faa2c68960e00483b1390fdaf4a991cc4"
    },

    {
      // URI with unreserved characters shouldn't be encoded
      "name" : "get-unreserved",
      "uri" : "http://pay-api.amazon.eu/-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
      "method" : "GET",
      "parameters" : {},
      "payload" : "",
      "canonicalRequest" : "GET\n/-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz\n\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n585e034d38ed3d64c0cd77a9f357a4b4a0fc093eebe06f4b06f66845e3543038"
    },

    {
      "name" : "get-vanilla-utf8-query",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "GET",
      "parameters" : {
        "\u1234" : [ "bar" ]
      },
      "payload" : "",
      "canonicalRequest" : "GET\n/\n%E1%88%B4=bar\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\nf0597d2fdcf97baf28002461796e916a74e35073b44042296d2ed45bacf6ecf0"
    },

    {
      // POSTing to a vanilla URI
      "name" : "post-vanilla",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "POST",
      "parameters" : {},
      "payload" : "",
      "canonicalRequest" : "POST\n/\n\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n322307b5dae22c3d59350c3de9202a488f337f674c8f430b186282008264bd2b"
    },

    {
      "name" : "post-vanilla-query",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "POST",
      "parameters" : {
          "foo" : [ "bar" ]
      },
      "payload" : "",
      "canonicalRequest" : "POST\n/\nfoo=bar\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n9d61b5abddbfdcaf7810a8e83dfffe0a5d5f272d2676c0550d974d5383e94cd5"
    },

    {
      "name" : "get-vanilla-empty-query-key",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "GET",
      "parameters" : {
          "foo" : [ "bar" ]
      },
      "payload" : "",
      "canonicalRequest" : "GET\n/\nfoo=bar\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\nae6be18184f88038a0114dd5417ef00d8e6a840c8e9c87c2d58b2f9ae9883442"
    },

    {
      "name" : "get-vanilla-query-order-value",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "GET",
      "parameters" : {
          "foo" : [ "b", "a" ]
      },
      "payload" : "",
      "canonicalRequest" : "GET\n/\nfoo=a&foo=b\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n937de37fb8aed8af4214c373580c72f40c525202d1975df6836ba6ed0062b902"
    },

    {
      "name" : "get-vanilla-query-order-key",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "GET",
      "parameters" : {
          "a" : [ "foo" ],
          "b" : [ "foo" ]
      },
      "payload" : "",
      "canonicalRequest" : "GET\n/\na=foo&b=foo\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n457de2535eedbd6ef25f5c8ec4653ee885b99838d722189928f4fdf8d3c3ef4f"
    },

    {
      "name" : "get-vanilla-query-order-sort-by-key",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "GET",
      "parameters" : {
          "A.1" : [ "foo" ],
          "A.2" : [ "foo" ],
          "A.10" : [ "foo" ]
      },
      "payload" : "",
      "canonicalRequest" : "GET\n/\nA.1=foo&A.10=foo&A.2=foo\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n43ae5290e6742d30e3822b62b6d759afedafbe01534f778175c336b455d632e7"
    },

    {
      "name" : "get-vanilla-query-order-key-case",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "GET",
      "parameters" : {
          "foo" : [ "Zoo", "aha" ]
      },
      "payload" : "",
      "canonicalRequest" : "GET\n/\nfoo=Zoo&foo=aha\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n7a4d89bd832cb93f2102f66bc8d3a0bc5f224c0bb758eab261dcd34a8ed2f117"
    },

    {
      "name" : "get-vanilla-query-unreserved",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "GET",
      "parameters" : {
          "-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" : [ "-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" ]
      },
      "payload" : "",
      "canonicalRequest" : "GET\n/\n-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz=-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\naa69ca06d93d9220f9aa16314d8556e18e1b0ae39802cd169bd3af6c08cd7164"
    },

    {
      "name" : "post-vanilla-query-nonunreserved",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "POST",
      "parameters" : {
          "@#$%^&+=/,?><`\";:\\|][{} " : [ "@#$%^&+=/,?><`\";:\\|][{} " ]
      },
      "algorithm" : "AMZN-PAY-RSASSA-PSS",
      "payload" : "",
      "canonicalRequest" : "POST\n/\n%40%23%24%25%5E%26%2B%3D%2F%2C%3F%3E%3C%60%22%3B%3A%5C%7C%5D%5B%7B%7D%20=%40%23%24%25%5E%26%2B%3D%2F%2C%3F%3E%3C%60%22%3B%3A%5C%7C%5D%5B%7B%7D%20\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n50709f5220d06911feee2d6aa8bf8453c8517f44527c0912e62b905d26c7e08f"
    },

    {
      "name" : "post-vanilla-query-space",
      "uri" : "http://pay-api.amazon.eu/",
      "method" : "POST",
      "parameters" : {
          "f oo" : [ "b ar" ]
      },
      "payload" : "",
      "canonicalRequest" : "POST\n/\nf%20oo=b%20ar\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
      "stringToSign" : "AMZN-PAY-RSASSA-PSS\n79481e4e38a053f7be8bda701c0d302d39df18bf468578b133c3be8e93fea86a"
    },
 ]