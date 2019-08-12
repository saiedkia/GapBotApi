# GapBotApi
Gap Messenger Bot Api in C# 


### Usage
- Install nuget package:
```
Install-Package GapBotApi
```

- Text message
```C#
GapClient gapClient = new GapClient("gap access token");
Message message = new Message()
{
    ChatId = "chat_Id",
    Data = "Hello World!!!"
};


PostResult result = gapClient.Send(message).Result;
```
- Upload file
```C#
GapClient gapClient = new GapClient(Token);
string fileDescription = "file description"; // 4.7MB

// step one: upload file to gap servers
PostResult uploadResult = gapClient.Upload("C:\gap_music.mp3", "gapFile_test_audio.mp3", UploadFileType.Audio, fileDescription).Result;
File file = Utils.Deserialize<File>(uploadResult.RawBody);
file.Desc = fileDescription;
Message message = new Message(MessageType.Audio)
{
    ChatId = ChatId,
    Data = Utils.Serialize(file)
};

// step two: send file to client
PostResult postResult = gapClient.Send(message).Result;
```

- Create invoice
```C#
 GapClient gapClient = new GapClient(Token);
Invoice invoice = new Invoice()
{
    ChatId = ChatId,
    Amount = 20_000,
    Currency = Currency.IRR,
    Description = "no comment"
};

PostResult invoiceResult = gapClient.Invoice(invoice).Result;
```

### MVC .NETCORE
- Install nuget package:
```
Install-Package GapBotApi.Mvc
```
```C#
[HttpPost]
public void Post([FromForm, FromFormReceivedMessage] ReceivedMessage receivedMessage)
{
 // do something
 }
```
