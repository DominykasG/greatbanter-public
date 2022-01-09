import React from 'react';
import ReactDOM from 'react-dom';

import { Provider } from 'react-redux';
import configureStore from './store/store';

import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import AuthProvider from './components/AuthProvider';

ReactDOM.render(
  <Provider store={configureStore}>
    <AuthProvider>
      <React.StrictMode>
        <App/>
      </React.StrictMode>
    </AuthProvider>
  </Provider>,
  document.getElementById('root')
);
reportWebVitals();
