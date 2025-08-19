import axios from "axios";
import { config } from '../constants/AppConstant'

const BASE_URL = config.url.API_URL_PRODUCTS;
axios.defaults.baseURL = BASE_URL;

export default axios;