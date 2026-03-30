hmLoadTopic({
hmKeywords:"",
hmTitle:"TanLinePointCircle- пример использования",
hmDescription:"void TanLinePointCircle_Example (void) {   double x[2], y[2]; int k, i;   Circle ( 60, 60, 30, 1); Point ( 20, 50, 1);   TanLinePointCircle (20, 50, 60, 60, 30, &k, x, y)",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">TanLinePointCircle- пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1717918\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void TanLinePointCircle_Example (void) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x[2], y[2];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int k, i;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Circle ( 60, 60, 30, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Point ( 20, 50, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">TanLinePointCircle (20, 50, 60, 60, 30, &amp;k, x, y);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">gprintf(Количество касаний = %2d, k);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (k)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">for (I=0; i&lt;k; I) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (20, 50, x[i], y[i], 1); \/* линия касания *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Point (x[i], y[i], 1); \/* точка касания *\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}; \/* TanLinePointCircle_Example *\/<\/span><\/p>\n\r"
})
