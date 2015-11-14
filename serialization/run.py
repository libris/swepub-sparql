import requests
from rdflib import ConjunctiveGraph
import json

def simplyframe(data):
    items, refs = {}, {}
    for item in data['@graph']:
        itemid = item.get('@id')
        if itemid:
            items[itemid] = item
        for vs in item.values():
            for v in [vs] if not isinstance(vs, list) else vs:
                if isinstance(v, dict):
                    refid = v.get('@id')
                    if refid:
                        refs.setdefault(refid, []).append(v)
    for refs in refs.values():
        if len(refs) == 1:
            ref = refs[0]
            ref.update(items.pop(ref['@id']))
            if  and refid.startswith('_:'):
                del ref['@id']
    data['@graph'] = items.values()

response = requests.post('http://virhp07.libris.kb.se/sparql', {
        'query': open('query.sparql').read(),
        'format': 'text/turtle'
    }, stream=True)

graph = ConjunctiveGraph()
graph.parse(response.raw, format='turtle')
output = graph.serialize(format='json-ld', context="context.jsonld")

data = json.loads(output)
simplyframe(data)
print(json.dumps(data, indent=2))
