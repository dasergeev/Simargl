hmLoadTopic({
hmKeywords:"",
hmTitle:"ViewPointer - пример использования",
hmDescription:"void ViewPointer_Example (void) { ViewPointerParam par; memset (&par, 0, sizeof (ViewPointerParam));   par.x1 = 55; par.y1 = 50; \/\/ координаты вершины (острия) стрелки par.",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ViewPointer - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1723925\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void ViewPointer_Example (void)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ViewPointerParam par;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">memset (&amp;par, 0, sizeof (ViewPointerParam));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.x1 = 55; par.y1 = 50; \/\/ координаты вершины (острия) стрелки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.x2 = 40; par.y2 = 50; \/\/ координаты конечной точки стрелки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.xt = 40; par.yt = 52; \/\/ координаты текста<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">strcpy(par.str, A); \/\/ надпись<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference p = ViewPointer (&amp;par); \/\/параметры стрелки вида<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ExistObj(p))<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj(p, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">};<\/span><\/p>\n\r"
})
