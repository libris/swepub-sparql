# SwePubs SPARQL-bibliotek
Här kommer SwePub-projektet - och kanske även du - att lägga upp SPARQL-frågor som kan vara användbara för andra.   
Källkoden för SwePubs webbgränssnitt hittar du på https://github.com/libris/swepubanalys

## INSTRUKTIONER FÖR DATAUTTAG
SwePub stödjer i dagsläget utsökning av originaldata. Utsökning av kvalitetssäkrad data, som utgörs av berikad och kontrollerad data, kommer att införas på sikt. Andelen kvalitetssäkrad data motsvarar knappt en fjärdedel av databasen.

### Åtkomst till länkad data
Data i SwePub är fritt tillgängligt för åtkomst och återanvändning. Data höstas in enligt SwePub MODS-format och utsökningar mappas från MODS XML. SPARQL endpoint: href=http://user:pw@virhp07.libris.kb.se/sparql-auth. För tillfällig inloggning ange användarnamn ‘user1’ och lösenord ‘guest’. För egna inloggningsuppgifter kontakta Libris kundservice. 
SPARQL Query Editor: href=http://virhp07.libris.kb.se/sparql-auth

Listor på förenklade MODS-element med benämningar och kommentarer:

Klass
Attribut
Relationer

### Skapa API av sökfråga
Ett API skapas enklast baserat på en utsökning som görs i utsökningsformuläret.

1. SPARQL-sökfrågan som ligger till grund för en utsökning visas på resultatsidan under fliken Sökfråga.
2. Kopiera sökfrågan och koda den till en URL, till exempel på http://meyerweb.com/eric/tools/dencoder/.
3. Lägg till följande i början på den skapade URL:en:  http://user:pw@virhp07.libris.kb.se/sparql-auth?query=
4. Lägg till följande i slutet på den skapade URL:en: &format=text%2Fcsv
5. Ange önskat format genom att byta ut det fetmarkerade ‘text’ och ‘csv’ enligt alternativen i Lista på tillgängliga format.
6. Testa API:et genom att klistra in URL:en i webbläsaren eller använd en HTTP-klient för integration med lokalt system. 

### Lista på tillgängliga format
* text/html
* text/csv
* text/plain
* text/tab-separated-values
* application/vnd.ms-excel 
* application/sparql-results+xml
* application/sparql-results+json
* application/rdf+xml

### Skapa sökfråga
Som underlag för att skapa egna sökfrågor för mer riktade analyser beskrivs delar av datamodellen. Den är strukturerad så att modellen i sin helhet har brutits ner i mindre delar. Varje del fokuserar på ett dataelement och visar praktiska exempel på hur en sökfråga skapas för den specifika delen av data. Se sidan Datamodell för en översikt.


#### Organisationer och deras poster

##### Datamodell baserad på originaldata i MODS format

[ReportingSites_raw_model]

##### Sökfråga
```
PREFIX mods_m: <http://swepub.kb.se/mods/model#>
SELECT DISTINCT
?Record
?_recordContentSourceValue
WHERE
{
?Record a mods_m:Record .
?Record mods_m:hasMetadata ?Metadata .
?Metadata mods_m:hasMods ?Mods .
?Mods mods_m:hasRecordInfo ?RecordInfo .
?RecordInfo mods_m:hasRecordContentSource ?RecordContentSource .
?RecordContentSource mods_m:recordContentSourceValue ?_recordContentSourceValue .
}
LIMIT 100
```

##### Visualiserad sökfråga

[ReportingSites_raw_query]

##### Resultatlista

| RECORD	| _RECORDCONTENTSOURCEVALUE
| -------------- | ----------------------------- |
| Record__oai_DiVA_org_liu60580_1 |	“liu” |
| Record__oai_DiVA_org_su107994_1 |	“su” |
| Record__oai_lup_lub_lu_se_4696391_1 |	“lu” |

#### Förkortad datamodell baserad på originaldata i MODS format

[ReportingSites_shortcutted_model]

##### Förkortad sökfråga
```
PREFIX mods_m: <http://swepub.kb.se/mods/model#>
SELECT DISTINCT
?Record
?_recordContentSourceValue
WHERE
{
?Record mods_m:hasMods ?Mods .
?Mods a mods_m:Mods .
?Mods mods_m:recordContentSourceValue ?_recordContentSourceValue .
?Record a mods_m:Record .
}
LIMIT 100
```
#####Visualiserad sökfråga

[ReportingSites_shortcutted_query]

#####Resultatlista

| RECORD |	_RECORDCONTENTSOURCEVALUE |
| ---------------- | -------------------------- |
| Record__oai_DiVA_org_su107994_1 |	“su” |
| Record__oai_lup_lub_lu_se_4696391_1 |	“lu” |
| Record__oai_lup_lub_lu_se_4690774_1 |	“lu” |

####Förenklad datamodell baserad på sökfråga i MODS format

[ReportingSites_enriched_model]

#####Förenklad sökfråga
```
PREFIX mods_m: <http://swepub.kb.se/mods/model#>
SELECT DISTINCT
?Record
?_id 
?_label
WHERE
{
?Record a mods_m:Record .
?Record swpa_m:reportingSite ?ResearchOrganization .
?ResearchOrganization swpa_m:hasIdentity ?Identity .
?Identity swpa_m:authority ?_authority .
?Identity swpa_m:id ?_id .
?ResearchOrganization rdfs:label ?_label .
FILTER(?_authority = "swepub"^^xsd:string)
FILTER(lang(?_label) = 'sv' )
}
LIMIT 100
```
#####Visualiserad sökfråga

[ReportingSites_enriched_query]

##### Resultatlista

| RECORD |	_ID |	_LABEL |
| -------- | ----------|-------------------|
| Record__oai_bth_se_forskinfo7C7A825038DE6570C12577E3004FFB7C_1 |	“bth” |	“Blekinge Tekniska Högskola” |
| Record__oai_bth_se_forskinfo2FF955D8AEEDAC82C12573C6005FBF6E_1 |	“bth” |	“Blekinge Tekniska Högskola” |
| Record__oai_bth_se_forskinfoA9B1D27F38B3E64BC1257310003EA166_1 |	“bth” |	“Blekinge Tekniska Högskola” | 



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

## Hur formulerar jag mina SPARQL-frågor?
En bra indroduktion och steg-för-steg-genomgång i hur du använder den visuella [Query-generatorn](http://hp07.libris.kb.se/ExploreAndQuery/) finner du på [LIBRIS-bloggen](http://librisbloggen.kb.se/2014/06/18/swepub-analysis-you-can-sparql/).

## Men all data är ju fraktionerad. Hur får jag ut riktiga poster med flervärdesfält?
Det finns många approacher till detta. SwePub projektet använder idag två.
### Lokalt datalager
Den första lösningen bygger på att man ser querysresultatet som en grund för ett lokalt datalager och flyttar över resultatet till tabeller i en lokal relationsdatabas. Det här används för att få ut data till webbversionen av analysverktyget SpotFire, som vi erbjuder alla med poster i SwePub.
### Serialisera queryresultatet
När SwePub internt serialiserar data för vidare indexering i Elasticsearch använder vi ett bibliotek som tar ett resultat i Turtle-format, letar reda på vilka triplar som hör till samma subjekt och slår ihop dessa till en "post" i JSON-LD-format.
TODO: Lägg in exempel i repot hur man gör.


