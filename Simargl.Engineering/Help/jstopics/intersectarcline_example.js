hmLoadTopic({
hmKeywords:"",
hmTitle:"IntersectArcLine - пример использования",
hmDescription:"void IntersectArcLin_Example (void) {   double x[2], y[2]; int k, i;   Arc ( 60, 60, 30, 0, 90, 1, 1); Line ( 20, 20, 45);   IntersectArcLin (60, 60, 30, 0, 90, 1, 20, 20",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">IntersectArcLine - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1717374\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void IntersectArcLin_Example (void) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x[2], y[2];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int k, i;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Arc ( 60, 60, 30, 0, 90, 1, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Line ( 20, 20, 45);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">IntersectArcLin (60, 60, 30, 0, 90, 1, 20, 20, 45, &amp;k, x, y);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[128];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">sprintf(buf, &quot;Количество пересечений = %i&quot;, k);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Message(buf);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (k)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">for (I=0; i&lt;k; I) Point (x[i], y[i], 1); \/* точки пересечения *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">};<\/span><\/p>\n\r"
})
