# csharp_webapi_read_documents

Web API that search fo words on an Azure Container .NET COE 5.0 and Razor pages

- Filter content Type Elements is a hack, I had two alternative hardcode the type that I know are available
  or create a list from the results, but I have to force all the elements.

```
PM> dotnet user-secrets list --project DocumentSearch
ConnectionStrings:ServiceName = demo-fr
ConnectionStrings:QueryKey = QUERYKEY
ConnectionStrings:PageSize = 3
ConnectionStrings:IndexName = INDEXID
ConnectionStrings:ContainerUrl =
```

Query key is frdemosearch created by me

## Models

- BlobDocument.cs
- PageItem.cs
- SearchResult.cs
- BlobSearchResult.cs
- SearchParam.cs
- Services
- SearchServices.cs
- Page
- Index

## Note:

I think it is very slow:
My suggestion are: remove the all search for the content type

- Download field is not sortable but the css was little hardcode
- javascript ignore the downlaod field
