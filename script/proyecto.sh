#!/bin/bash
# Execution Functions
function run(){
    echo
    echo -e "\e[32mLaunching Moogle!...\e[0m"
    cd ..
    dotnet watch run --project MoogleServer
    clear
    echo -e "\e[31mStopped Moogle! Execution.\e[0m"
    cd script
}

function report(){
    echo
    echo -e "\e[32mBuilding Report...\e[0m"
    cd ..
    cd Informe
    latexmk -pdf Informe.tex
    clear
    echo -e "\e[32mBuilt Report.\e[0m"
    cd .. 
    cd script
}

function slides(){
    echo
    echo -e "\e[32mBuilding Slides...\e[0m"
    cd ..
    cd Presentacion
    latexmk -pdf Presentacion.tex
    clear
    echo -e "\e[32mBuilt Slides.\e[0m"
    cd ..
    cd script
}

function show_report(){
    clear
    echo
    echo -e "\e[32mShowing Report...\e[0m"
    cd .. 
    cd Informe
    xdg-open Informe.pdf
}

function show_slides(){
    clear
    echo
    echo -e "\e[32mShowing Slides...\e[0m"
    cd ..
    cd Presentacion
    xdg-open Presentacion.pdf
}

function clean(){
    echo
    echo -e "\e[32mCleaning Temporal Files...\e[0m"
    cd ..
    cd Informe
    rm Informe.aux Informe.fdb_latexmk Informe.fls Informe.log indent.log pdflatex32230.fls Informe.synctex.gz
    cd ..
    cd Presentacion
    rm Presentacion.aux Presentacion.fdb_latexmk Presentacion.fls Presentacion.log Presentacion.nav Presentacion.snm Presentacion.toc Presentacion.out Presentacion.synctex.gz
    cd sections
    rm arch.aux dataflow.aux intro.aux
    cd ..
    clear
    echo -e "\e[32mTemporal Files Cleaned.\e[0m"
    cd ..
    cd script

}

# Interactive Function
function interactive(){
    while true; do
        clear
        echo "Type the option number you want to perform:"
        echo
        echo "1) run              -Launch Moogle!"
        echo "2) report           -Build Moogle! Report"
        echo "3) slides           -Build Moogle! Slides" 
        echo "4) show_report      -Show Moogle! Report"
        echo "5) show_slides      -Show Moogle! Slides"
        echo "6) clean            -Clean Temporal Files"
        echo "7) quit             -Exit"
        echo "-----------Type Below-----------------------------------"

        read option
        case $option in 
            1)
                run
                echo -e "\e[32mPress Enter to continue\e[0m"
                read
                ;;
            2)
                report
                echo -e "\e[32mPress Enter to continue\e[0m"
                read
                ;;
            3) 
                slides
                echo -e "\e[32mPress Enter to continue\e[0m"
                read
                ;;	
            4)
                show_report
                start Informe.pdf
                echo -e "\e[32mPress Enter to continue\e[0m"
                cd ..
                cd script
                read
                ;;
            5)
                show_slides
                start Presentacion.pdf
                echo -e "\e[32mPress Enter to continue\e[0m"
                cd ..
                cd script
                read
                ;;
            6)
                clean
                echo -e "\e[32mPress Enter to continue\e[0m"
                read
                ;;
            7) 
                echo -e "\e[31mClosing..."
                clear
                echo -e "\e[31mClosed..."
                break
                ;; 
            *)
                echo -e "\e[31mWRONG INPUT...\e[0m"
                echo -e "\e[31mPress Enter and type again...\e[0m"
                read
        esac

    done
}


FUNCTIONS="run report slides show_report show_slides clean interactive"

execute=$1
# If there's no function, show available functions
if [ "$execute" = "" ]; then
    echo
    echo "Functions:"
    for i in $FUNCTIONS; do
        echo "-$i"
    done
    exit 1
fi


case $execute in
    run)
        run
        ;;
    report)
        report
        ;;
    slides)
        slides
        ;;
    show_report)
        show_report
        start Informe.pdf
        ;;
    show_slides)
        show_slides
        start Presentacion.pdf
        ;;
    clean)
        clean
        ;;
    interactive)
        interactive
        ;;
    *)
        echo
        echo -e "\e[31mWRONG FUNCTION...\e[0m"
esac