hmLoadTopic({
hmKeywords:"",
hmTitle:"ksDrawKompasDocument - пример использования",
hmDescription:"TWindow *st = new TWindow (GetWindowPtr( ( HWND )GetHWindow( ) ), 0 );",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksDrawKompasDocument - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1731921\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">TWindow *st = new TWindow (GetWindowPtr( ( HWND )GetHWindow( ) ), 0 );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">st-&gt;Attr.X =300;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">st-&gt;Attr.Y =30;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">st-&gt;Attr.W =170;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">st-&gt;Attr.H =160;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">st-&gt;Create( ); \/\/создали окно, в котором хотим отрисовать слайд<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksDrawKompasDocument( ( void* ) st-&gt;HWindow, \/\/ несущее окно<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&quot;d:\\\\0\\\\fg1.frw&quot; ); \/\/ полное имя файла документа<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message( &quot;Слайд КОМПАС&quot; );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">delete st;<\/span><\/p>\n\r"
})
