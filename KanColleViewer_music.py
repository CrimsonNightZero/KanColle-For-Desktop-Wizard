import requests
import os
import shutil
from bs4 import BeautifulSoup as bs
import multiprocessing 

def allow(wikitable4):
    ill_sign = ["播放", "？", "暂缺", "！", "。", "…"]
    for ill in ill_sign:
        if ill  in wikitable4.text:
            return False
    return True

def xml_write(path, date_current):
    if(date_current is None):
        file = open(path, "a")
        file.close()
    else:
        file = open(path, "w")
        file.write(date_current + "\n")
        file.close()
        
def rowspan(td, rowspan_count):
    if td[0].has_attr('rowspan'):
        rowspan_count = int(td[0]['rowspan']) - 1
        return True, rowspan_count
    else:
        if rowspan_count == 0:
            return True, rowspan_count
        else :
            rowspan_count -= 1
            return False, rowspan_count
        
def illustration_page(href,Path2,site):
    import re
    site2 = site[0:len(site)-1]+href
    res2 = requests.get(site2)
    soup2 = bs(res2.text,"html.parser") 
    tables = soup2.find_all("table", class_ = re.compile("wikitable"))
    ##print(a)
    folder = None
    music = None
    rowspan_count = 0
    
    for table in tables:
        for tr in table.find_all('tr'):
            td = tr.find_all('td')
            
            if td == [] :
                continue
            elif "：" in td[0].text:
                folder = folder
            else:
                td_span, rowspan_count = rowspan(td, rowspan_count)
                if (td_span):
                    folder = td[0].text.replace("\n","").replace(" ","").replace("/","_")
                else:
                    folder = folder
            
            for li in tr.find_all("li"):
                for a in li.find_all('a'):
                    if a.has_attr('data-filesrc'): 
                        if "https://img.moegirl.org/" in a['data-filesrc']:
                            music = a['data-filesrc']
                            print(music)
            if music is None:
                continue
            print("11")
            if not os.path.isdir(os.path.join(Path2, folder)):
                print(folder)
                os.makedirs(r'%s/%s'%(Path2,folder))        
                
            name = music.split("/")[-1]
            if not os.path.isfile(os.path.join(Path2, folder, name)):
                music_path = os.path.join(Path2, folder, "%s")
                try:
                    res3 = requests.get(music, stream = True )
                    print("music_path:")
                    print(music_path)
                
                    with open(music_path  %(name), "wb") as f:
                        shutil.copyfileobj(res3.raw, f)
                except requests.exceptions.MissingSchema as error:
                    xml_path =  r"C:\Users\foryou\KanColleViewer_error.xml"
                    xml_write(xml_path, music)
    return "ok"
                
def main():
    Path = r"D:\KanColleViewer"
    #Path = r"C:\Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Music"
    site = "https://zh.moegirl.org/zh-tw/"
    res = requests.get(site + "%E8%88%B0%E9%98%9FCollection/%E5%9B%BE%E9%89%B4/%E8%88%B0%E5%A8%98")
    soup = bs(res.text, "html.parser")
    
    #folder = "null"
    results = []
    cpus = multiprocessing.cpu_count()
    pool = multiprocessing.Pool(processes = int(cpus * 1.5))
    #主頁尋找圖鑑
    
    i = 0
    for wikitable in soup.select(".wikitable"):
        for url in wikitable.select("a"):
            if url.text in "No." and "Collection:" in url['href']:
                href = url['href']
                name = url['title'].split(":")[1]
                Path2 = os.path.join(Path, name)
                if not os.path.isdir(os.path.join(Path, name)):
                    print("Path:" + Path)
                    os.makedirs(r'%s/%s'%(Path, name))
                    print(name)
                print(name, Path2, href)
                
#                t = threading.Thread(target = illustration_page, args = (url,site))
#                t.start()
#                i += 1
#                if i > 308:
#                    illustration_page(href, Path2, site)
#                    return
                result = pool.apply_async(func = illustration_page, args = (href, Path2, site))
                results.append(result)
                i += 1
                
                if(i % 10 == 0):
                    for result in results:
                        print(result.get())     
        results = []
    pool.close()
    pool.join()
    #counter=0
    
if __name__ == '__main__':
    __spec__ = "ModuleSpec(name='builtins', loader=<class '_frozen_importlib.BuiltinImporter'>)"
    xml_path =  r"C:\Users\foryou\KanColleViewer_error.xml"
    xml_write(xml_path,None)
    main()
    print("Finish")
    
    
    
                