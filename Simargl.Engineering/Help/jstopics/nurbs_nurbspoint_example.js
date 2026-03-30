hmLoadTopic({
hmKeywords:"",
hmTitle:"Nurbs, NurbsPoint - пример использования",
hmDescription:"void Nurbs_Example (void) {   static NurbsPointParam par[]={{ 0,0,1}, {20,20,1}, {50,10,1}, {70,20,1}, {100,0,1}, {50,-50,1}};   \/\/построить Nurbs-кривую if (Nurbs(3, 0, 1))",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">Nurbs, NurbsPoint - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1720664\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void Nurbs_Example (void) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">static NurbsPointParam par[]={{ 0,0,1}, {20,20,1}, {50,10,1}, {70,20,1}, {100,0,1}, {50,-50,1}};<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/построить Nurbs-кривую<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (Nurbs(3, 0, 1))<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">for (int i=0; i&lt;6; i) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">NurbsPoint(&amp;par[i]);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference p = EndObj();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj(p, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message(NURBS);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj(p, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}; \/* Nurbs_Example *\/<\/span><\/p>\n\r"
})
