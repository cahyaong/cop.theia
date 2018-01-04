import os
import pandas as pd
import sqlite3
from .chemical_compound import ChemicalCompound
from .cop_error import CopError


class ChemblRepository:

    _db_connection = None

    def __init__(self, data_folder_path):
        db_file_path = os.path.abspath(os.path.join(data_folder_path, 'chembl_23.db'))

        if os.path.isfile(db_file_path):
            self._db_connection = sqlite3.connect(db_file_path)
        else:
            raise CopError('Invalid path to ChEMBL data source! Path: [{0}].'.format(db_file_path))

    def get_total_compound_count(self):
        query = """
            SELECT COUNT(0)
            FROM compound_records"""

        cursor = self._db_connection.cursor()
        cursor.execute(query)

        return int(cursor.fetchone()[0])

    def get_remaining_compound_count(self, last_processed_id):
        query = """
            SELECT COUNT(0)
            FROM compound_records
            WHERE molregno > """ + str(last_processed_id)

        cursor = self._db_connection.cursor()
        cursor.execute(query)

        return int(cursor.fetchone()[0])

    def get_compounds(self, last_processed_id, batch_size):
        assert batch_size > 0

        query = """
            SELECT molregno AS id, canonical_smiles
            FROM compound_structures
            WHERE molregno > """ + str(last_processed_id) + """
            ORDER BY molregno
            LIMIT """ + str(batch_size)

        compounds = []

        for index, row in pd.read_sql_query(query, self._db_connection).iterrows():
            compound = ChemicalCompound()
            compound.id = int(row['id'])
            compound.canonical_smiles = str(row['canonical_smiles'])
            compounds.append(compound)

        return compounds
