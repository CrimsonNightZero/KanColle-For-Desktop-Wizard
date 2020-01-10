import requests
from bs4 import BeautifulSoup as bs
import os
import shutil
res=requests.get("http://threebards.com/kaini/furniture/")
soup=bs(res.text,"html.parser")
url=list(range(100))
url2=list(range(100))
i=0
Path="C:\\Users\\foryou\\Documents\\Visual Studio 2015\\Projects\\KanColleViewer\\KanColleViewer\\bin\\Debug\\Furniture"

for furniture in soup.select('a'):
	if "kaini" in furniture['href'] or "outside" in furniture['href']:
		continue
	url[i]="http://threebards.com/kaini/furniture/"+furniture['href'];
	res2=requests.get(url[i])
	soup2=bs(res2.text,"html.parser")
	print(str(Path)+"\\"+str(furniture['href']))
	if False==os.path.isdir(Path+"\\"+furniture['href'].replace("\\","")):
		os.makedirs(r'%s/%s'%(Path,furniture['href']))
	for furniture2 in soup2.select('a'):
		if "kaini" in furniture2['href'] or "000" in furniture2['href']:
			continue
		url2[i]=url[i]+furniture2['href'];
		res3=requests.get(url2[i],stream=True)
		##soup3=bs(res3.text,"html.parser")
		print(url2[i])
		name=furniture2['href']
		path2=Path+"\\"+furniture['href'].replace("/","\\")+"%s"
		print(str(path2)+","+name)
		f=open(path2 %(name),"wb")
		shutil.copyfileobj(res3.raw,f)
		print(str(furniture2['href']))
		
	i+=1
f.close()