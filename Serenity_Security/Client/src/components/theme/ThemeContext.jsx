import React, { createContext, useState, useContext } from 'react';
import DarkMatrixBackground from './DarkMatrixBackground';
import LightMatrixBackground from './LightMatrixBackground';

const ThemeContext = createContext();

export const useTheme = () => useContext(ThemeContext);

export const ThemeProvider = ({ children }) => {
  const [isDarkMode, setIsDarkMode] = useState(true);

  const toggleTheme = () => {
    setIsDarkMode(!isDarkMode);
  };

  const theme = {
    isDarkMode,
    toggleTheme,
    colors: {
      primary: '#1b2a3c', // Navy
      secondary: '#ffd365', // Yellow
      background: isDarkMode ? '#121212' : '#fdf6e3', // Dark/Cream
      text: isDarkMode ? '#1b2a3c' : '#fdf6e3', // Text adapts to theme
      cardBg: isDarkMode 
        ? '#fdf6e3' // Cream for dark mode cards
        : '#1b2a3c', // Navy for light mode cards
      cardText: isDarkMode
        ? '#1b2a3c' // Navy text for dark mode cards 
        : '#fdf6e3', // Cream text for light mode cards
      buttonHighlight: '#ffd365' // Yellow for buttons
    }
  };

  return (
    <ThemeContext.Provider value={theme}>
      {isDarkMode ? <DarkMatrixBackground /> : <LightMatrixBackground />}
      <div style={{ 
        color: isDarkMode ? '#fdf6e3' : '#1b2a3c', // Text color outside cards: cream in dark mode, navy in light mode
        position: 'relative',
        zIndex: 1,
        minHeight: '100vh'
      }}>
        {children}
      </div>
    </ThemeContext.Provider>
  );
};

export default ThemeContext;