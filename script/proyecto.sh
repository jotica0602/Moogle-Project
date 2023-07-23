#!/bin/bash
while true; do
    clear
    echo "Type the option you want to perform:"
    echo
    echo "-run"
    echo "-report"
    echo "-slides"
    echo "-show_report"
    echo "-show_slides"
    echo "-clean"
    echo "-quit"
    echo

    read option
    case $option in 
        run)
            echo -e "\e[32m Launching Moogle!...\e[0m"
            cd ..
            dotnet watch run --project MoogleServer
            echo
            echo
            echo "Press Enter to continue" 
            cd script
            read
            ;;
        report)
            echo -e "\e[32m Compiling Report...\e[0m"
            cd .. 
            cd Informe
            latexmk -pdf Informe.tex
            echo "Press Enter to continue"
            cd ..
            cd script
            read
            ;;
        slides) 
            echo -e "\e[32m Compiling Slides\e[0m"
            cd ..
            cd Presentacion
            cd ..
            cd script
            echo "Press Enter to continue"
            read
            ;;	
        show_report)
            echo -e "\e[32m Showing Report...\e[0m"
            cd ..
            cd Informe
            xdg-open Informe.pdf
            start Informe.pdf
            cd ..
            cd script
            echo "Press Enter to continue"
            read
            ;;
        show_slides)
            echo -e "\e[32m Showing Slides...\e[0m"
            cd ..
            cd Presentacion
            #xdg-open Presentacion
            cd ..
            cd script
            echo "Press Enter to continue"
            read
            ;;
        clean)
            echo -e "\e[32m Cleaning Temporal Files...\e[0m"
            cd ..
            cd Informe
            rm Informe.aux Informe.fdb_latexmk Informe.fls Informe.log
            cd ..
            cd Presentacion
            #Borrar
            echo "Press Enter to continue"
            read
            cd ..
            cd script
            ;;
        quit) 
            echo -e "\e[32m Closing..."
            break
            ;; 
        *)
            echo -e "\e[31m WRONG INPUT...\e[0m"
            read
    esac
done




