hmLoadTopic({
hmKeywords:"",
hmTitle:"MtrForIGES - Пример использования",
hmDescription:"double commonArray[2][2]; \/\/ матрица 2*2 поворота,",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">MtrForIGES - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1721088\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double commonArray[2][2]; \/\/ матрица 2*2 поворота,<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/умноженная предварительно на масштаб<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double moveArray[2]; \/\/ матрица сдвига умноженная предварительно на масштаб<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ заполним матрицы<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">commonArray[0][0] = 2.0;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">commonArray[0][1] = 0.0;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">commonArray[1][0] = 0.0;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">commonArray[1][1] = -2.0;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">moveArray[0] = 20.0; \/\/ сдвиг по X<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">moveArray[1] = 0.0; \/\/ сдвиг по Y<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg(0, 0, 10, 10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/создание матрицы трансформации - симметрия по Х и с масштабом 2 и сдвиг на 10мм<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::MtrForIGES(commonArray, moveArray)) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg(0, 0, 10, 10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">DeleteMtr();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
