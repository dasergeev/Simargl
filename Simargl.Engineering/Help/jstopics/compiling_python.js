hmLoadTopic({
hmKeywords:"",
hmTitle:"Python",
hmDescription:"Для получения возможности использования API Компас в Python требуется выполнить импорт с tlb-файлов ksConstants.tlb, ksConstants3D.tlb, kAPI5.tlb, kAPI7.tlb:",
hmPrevLink:"compiling_visual_c.html",
hmNextLink:"vc_recommendations.html",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"<a href=\"compiling_libraries.html\">Компиляция библиотек<\/a>",
hmTitlePath:"Компиляция библиотек > Python",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">Python<\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><span class=\"f_bodytext\">Для получения возможности использования API Компас в Python требуется выполнить импорт с tlb-файлов <\/span><span class=\"f_bodytext\" style=\"font-style: italic;\">ksConstants.tlb<\/span><span class=\"f_bodytext\">, <\/span><span class=\"f_bodytext\" style=\"font-style: italic;\">ksConstants3D.tlb<\/span><span class=\"f_bodytext\">, <\/span><span class=\"f_bodytext\" style=\"font-style: italic;\">kAPI5.tlb<\/span><span class=\"f_bodytext\">, <\/span><span class=\"f_bodytext\" style=\"font-style: italic;\">kAPI7.tlb<\/span><span class=\"f_bodytext\">:<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">from win32com.client import gencache<\/span><br \/>\n\r<span class=\"f_bodytext\">kompas6_constants = gencache.EnsureModule(&quot;{75C9F5D0-B5B8-4526-8681-9903C567D2ED}&quot;, 0, 1, 0).constants<\/span><br \/>\n\r<span class=\"f_bodytext\">kompas6_constants_3d = gencache.EnsureModule(&quot;{2CAF168C-7961-4B90-9DA2-701419BEEFE3}&quot;, 0, 1, 0).constants<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">kompas6_api5_module = gencache.EnsureModule(&quot;{0422828C-F174-495E-AC5D-D31014DBBE87}&quot;, 0, 1, 0)<\/span><br \/>\n\r<span class=\"f_bodytext\">kompas_api7_module = gencache.EnsureModule(&quot;{69AC2981-37C0-4379-84FD-5DD2F3C0A520}&quot;, 0, 1, 0)<\/span><\/p>\n\r"
})
