hmLoadTopic({
hmKeywords:"",
hmTitle:"C++",
hmDescription:"В C++ для работы с интерфейсами удобнее всего использовать \"умные указатели\".",
hmPrevLink:"cr109297.html",
hmNextLink:"cr109321.html",
hmParentLink:"recomendation.html",
hmBreadCrumbs:"<a href=\"libraries_create.html\">Создание прикладных библиотек&nbsp;<\/a> &gt; <a href=\"cr69365.html\">Рекомендации по созданию прикладных библиотек&nbsp;<\/a> &gt; <a href=\"recomendation.html\">Рекомендации по использованию метода IUnknown::QueryInterface&nbsp;<\/a>",
hmTitlePath:"Создание прикладных библиотек  > Рекомендации по созданию прикладных библиотек  > Рекомендации по использованию метода IUnknown::QueryInterface  > C++ ",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">C++ <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><span class=\"f_bodytext\">В C++ для работы с интерфейсами удобнее всего использовать &quot;умные указатели&quot;.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Они умеют выполнять QueryInterface и правильно выполняют захват и освобождение интерфейсов, используя методы AddRef и Release.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">IKompasDocumentPtr kompasDoc( KomApp.ActiveDocument );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">IDrawingDocumentPtr doc2D( kompasDoc ); \/\/ Здесь выполнится QueryInterface<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">или<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">IDrawingDocumentPtr doc2D( KomApp.ActiveDocument ); \/\/ Здесь выполнится QueryInterface.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_Z_LOC_TOC_Title\" style=\"border-top: none; border-right: none; border-left: none;\"><span class=\"f_Z_LOC_TOC_Title\">Подразделы:<\/span><\/p>\n\r<p class=\"p_Z_LOC_TOC\"><span class=\"f_Z_LOC_TOC\">(отсутствуют)<\/span><\/p>\n\r"
})
