# SwePubs SPARQL-bibliotek
Här kommer SwePub-projektet - och kanske även du - att lägga upp SPARQL-frågor som kan vara användbara för andra.   
Källkoden för SwePubs webbgränssnitt hittar du på https://github.com/libris/swepubanalys

## Hur formulerar jag mina SPARQL-frågor?
Se genomgången i Instruktioner för datauttag nedan. 
En bra indroduktion och steg-för-steg-genomgång i hur du använder den visuella [Query-generatorn](http://hp07.libris.kb.se/ExploreAndQuery/) finner du annars på [LIBRIS-bloggen](http://librisbloggen.kb.se/2014/06/18/swepub-analysis-you-can-sparql/).

### Men all data är ju fraktionerad. Hur får jag ut riktiga poster med flervärdesfält?
Det finns många approacher till detta. SwePub projektet använder idag två.

#### Lokalt datalager
Den första lösningen bygger på att man ser querysresultatet som en grund för ett lokalt datalager och flyttar över resultatet till tabeller i en lokal relationsdatabas. Det här används för att få ut data till webbversionen av analysverktyget SpotFire, som vi erbjuder alla med poster i SwePub.
#### Serialisera queryresultatet
När SwePub internt serialiserar data för vidare indexering i Elasticsearch använder vi ett bibliotek som tar ett resultat i Turtle-format, letar reda på vilka triplar som hör till samma subjekt och slår ihop dessa till en "post" i JSON-LD-format.
TODO: Lägg in exempel i repot hur man gör.


## Hur ställer jag SPARQL-frågor mot SwePub?
För att ställa frågor mot SwePub kan man antingen använda gränssnittet på http://info.swepub.kb.se eller gå direkt mot SwePubs Sparql-endpoint på http://virhp07.libris.kb.se/sparql/. 

Endpointen fungerar även som ett API. Man kan ställa frågor genom både POST och GET-anrop till samma url. 
Nedan ett exempel på anrop genom i Groovy med paketet WsLite som returnerar svaret i json-format:    

       def client = new RESTClient(http://virhp07.libris.kb.se/sparql)
        def response = client.post(
            accept: contentType = "application/json",
            path: '/',
            query: [query: sparql, format: contentType])
        assert 200 == response.statusCode
        return response.json   




