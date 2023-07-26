#!/bin/bash
# return to .sh folder
goback(){
    cd ..
    cd script
}

# execution Functions
run(){
    echo -e "\e[32mLaunching Moogle!...\e[0m"
    cd ..
    dotnet watch run --project MoogleServer
    clear
    echo -e "\e[31mStopped Moogle! Execution.\e[0m"
    cd script
}

report(){
    echo -e "\e[32mBuilding Report...\e[0m"
    cd ..
    cd Informe
    latexmk -pdf Informe.tex
    clear
    echo -e "\e[32mBuilt Report.\e[0m"
    goback
}

slides(){
    echo -e "\e[32mBuilding Slides...\e[0m"
    cd ..
    cd Presentacion
    latexmk -pdf Presentacion.tex
    clear
    echo -e "\e[32mBuilt Slides.\e[0m"
    goback
}

show_report(){
    clear
    echo -e "\e[32mShowing Report...\e[0m"
    cd ..
    cd Informe
    
    # if there's no report, build it.
    if [ ! -f  "Informe.pdf" ];
    then 
        report;
    fi

    cd ..
    cd Informe
    # default OS pdf viewer

    if [ -z "$1" ]; then
        echo -e "\e[33mDefault pdf viewer.\e[0m"
        if [[ "$OSTYPE" == "linux-gnu"* ]]; then
            xdg-open Informe.pdf
        elif [[ "$OSTYPE" == "darwin"* ]]; then
            open Informe.pdf
        elif [[ "$OSTYPE" == "cygwin" || "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
            start Informe.pdf
        else
            echo -e "\e[32mUnknown OS.\e[0m"
        fi
    else
        echo -e "\e[33mUser specified pdf viewer.\e[0m"
        if [[ "$OSTYPE" == "linux-gnu"* ]]; then
            $1 Informe.pdf
        elif [[ "$OSTYPE" == "darwin"* ]]; then
            open -a $1 Informe.pdf
        elif [[ "$OSTYPE" == "cygwin" || "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
            start $1 Informe.pdf
        else
            echo -e "\e[32mUnknown OS.\e[0m"
        fi
    fi
}

show_slides(){
    clear
    cd ..
    cd Presentacion

    # if there's no slides, build it.
    if [ ! -f "Presentacion.pdf" ];
    then
        slides;
    fi

    cd ..
    cd Presentacion
    echo -e "\e[32mShowing Slides...\e[0m"

    # default OS slides viewer
    if [ -z "$1" ]; then
        echo -e "\e[33mDefault pdf viewer.\e[0m"
        if [[ "$OSTYPE" == "linux-gnu"* ]]; then
            xdg-open Presentacion.pdf
        elif [[ "$OSTYPE" == "darwin"* ]]; then
            open Presentacion.pdf
        elif [[ "$OSTYPE" == "cygwin" || "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
            start Presentacion.pdf
        else
            echo -e "\e[32mUnknown OS.\e[0m"
        fi
    else
        echo -e "\e[33mUser specified pdf viewer.\e[0m"
        if [[ "$OSTYPE" == "linux-gnu"* ]]; then
            $1 Presentacion.pdf
        elif [[ "$OSTYPE" == "darwin"* ]]; then
            open -a $1 Presentacion.pdf
        elif [[ "$OSTYPE" == "cygwin" || "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
            start $1 Presentacion.pdf
        else
            echo -e "\e[32mUnknown OS.\e[0m"
        fi
    fi
}

clean(){
    echo -e "\e[33mCleaning Temporal Files...\e[0m"
    cd ..
    cd Informe
    rm Informe.aux Informe.fdb_latexmk Informe.fls Informe.log indent.log pdflatex32230.fls Informe.synctex.gz Informe.pdf
    cd ..
    cd Presentacion
    rm -f Presentacion.aux Presentacion.fdb_latexmk Presentacion.fls Presentacion.log Presentacion.nav Presentacion.snm Presentacion.toc Presentacion.out Presentacion.synctex.gz Presentacion.pdf
    cd sections
    rm -f arch.aux dataflow.aux intro.aux
    cd ..
    clear
    echo -e "\e[33mTemporal Files Cleaned.\e[0m"
    goback

}

# interactive Function
interactive(){
    while true; do
        clear
        echo "Type the function number you want to perform:"
        echo "1) run              -Launch Moogle!"
        echo "2) report           -Build Moogle! Report"
        echo "3) slides           -Build Moogle! Slides" 
        echo "4) show_report      -Show Moogle! Report"
        echo "5) show_slides      -Show Moogle! Slides"
        echo "6) clean            -Clean Temporal Files"
        echo "7) quit             -Exit"
        echo "Note: show_report and show_slides functions will only use default OS viewer."
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
                echo -e "\e[32mPress Enter to continue\e[0m"
                goback
                read
                ;;
            5)
                show_slides 
                echo -e "\e[32mPress Enter to continue\e[0m"
                goback
                read
                ;;
            6)
                clean
                echo -e "\e[32mPress Enter to continue\e[0m"
                read
                ;;
            7) 
                echo -e "\e[31mClosinge\e[0m..."
                clear
                echo -e "\e[31mClosed\e[0m..."
                break
                ;; 
            *)
                echo -e "\e[31mWRONG INPUT...\e[0m"
                echo -e "\e[31mPress Enter and type again...\e[0m"
                read
        esac

    done
}

FUNCTIONS="run report slides show_report show_slides clean"

execute=$1
# If there's no parameter, show available functions
if [ "$execute" = "" ]; then
    echo "Functions:"
    for i in $FUNCTIONS; do
        echo "-$i"
    done
    exit 1
fi

"$@"