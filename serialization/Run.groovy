@Grab('com.github.jsonld-java:jsonld-java:0.7.0')
@Grab('com.github.groovy-wslite:groovy-wslite:1.1.2')
import com.github.jsonldjava.core.JsonLdOptions
import com.github.jsonldjava.core.JsonLdProcessor
import com.github.jsonldjava.utils.JsonUtils
import wslite.rest.ContentType
import wslite.rest.RESTClient

println "Init done"
def turtle = makeSparqlRequest(new File("query.sparql").text)
def expanded = JsonLdProcessor.fromRDF(turtle,
        [format: "text/turtle", useNativeTypes: true] as JsonLdOptions)
Map context = new File("context.jsonld").withInputStream JsonUtils.&fromInputStream
def compacted = JsonLdProcessor.frame(expanded, context, [embed: true] as JsonLdOptions)

println turtle + "\n=====================\n"
println expanded + "\n=====================\n"
println JsonUtils.toPrettyString(compacted)


public String makeSparqlRequest(def sparql) {
	RESTClient client = new RESTClient('http://virhp07.libris.kb.se/sparql')
	def response = client.post(
	        accept: ContentType.TEXT,
	        path: '/',
	        query: [query: sparql, format: "text/turtle"])
	assert 200 == response.statusCode
	return response.text
}
